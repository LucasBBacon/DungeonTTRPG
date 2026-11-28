using System;
using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
using Dungeon.Core.Conditions;
using Dungeon.Core.Effects;

namespace Dungeon.Core;

public enum CommandError
{
    None,
    UnknownUnit,
    OutOfBounds,
    Blocked,
    Occupied,
    CornerCut,
    DecisionPending,
    NoDecisionPending,
    WrongDecision,
    InvalidOption,

    /// <summary>The run has ended (DH 10.1, D-08); every command is refused.</summary>
    RunEnded,

    /// <summary>In combat, the command names a unit whose turn it is not.</summary>
    NotYourTurn,

    /// <summary>In combat, the moving unit has no movement left this turn.</summary>
    NoMovementLeft,

    /// <summary>
    /// The command is valid only in combat; for an ability, a use out of combat that would draw (M1-11 plan call 5; D-23
    /// ruled 2026-11-28: the refusal stays in M1; the D-23 wave uses <c>h:roll</c> (M1-11 rulings record 2)).
    /// </summary>
    NotInCombat,

    // M1-11 plan call 3: the ability reason codes, named as their greyed-reason ids. AB 14.3's order is user, ability,
    // turn, cost, pools, camp spend, target shape, range, line of sight, then NotInCombat.

    /// <summary>The user does not hold the ability, or the rules have no such ability.</summary>
    UnknownAbility,

    /// <summary>The ability is a reaction, used only at its trigger, never by command.</summary>
    ReactionOnly,

    /// <summary>The ability costs the action, and the action is spent.</summary>
    NoActionLeft,

    /// <summary>The ability costs the bonus action, and it is spent.</summary>
    NoBonusActionLeft,

    /// <summary>The ability costs the free interaction, and it is spent.</summary>
    NoFreeInteraction,

    /// <summary>The ability's pool has fewer uses left than it spends.</summary>
    NoUsesLeft,

    /// <summary>The ability spends a camp pool, and the unit has already spent one this turn (AB 6.4).</summary>
    CampSpendUsed,

    /// <summary>The target does not fit the ability's shape.</summary>
    InvalidTarget,

    /// <summary>A target unit or point is beyond the ability's range.</summary>
    OutOfRange,

    /// <summary>The user cannot see a target unit or point.</summary>
    NoLineOfSight,

    /// <summary>A reaction policy names an ability that is not a reaction (M1-11 plan call 13).</summary>
    NotAReaction,

    /// <summary>"Ask me" is set on a unit that is not a party unit (AB 7.3: monsters have no "ask me").</summary>
    AskMeIsPartyOnly,

    /// <summary>"While the pool is at least half" is set on a reaction that has no pool, so it could never fire.</summary>
    PolicyNeedsPool,
}

/// <summary>
/// The outcome of applying a command. On rejection <see cref="State"/> is the same instance
/// that was passed in, <see cref="Events"/> is empty and <see cref="Error"/> says why.
/// </summary>
public sealed record StepResult(GameState State, ImmutableArray<GameEvent> Events, CommandError Error)
{
    public bool Ok => Error == CommandError.None;
}

/// <summary>The rules: a pure function from a state and a command to the next state and its events.</summary>
public static class Engine
{
    /// <summary>How many boons a shrine offers.</summary>
    internal const int ShrineBoonChoices = 3;

    /// <summary>Boon ids run from 0 to one below this.</summary>
    internal const int BoonCount = 10;

    /// <summary>
    /// Applies <paramref name="command"/> with no effect rules: <see cref="Apply(GameState, Command, RulesContext)"/>
    /// with <see cref="RulesContext.Empty"/> (M1-07 plan call 6), so a state without effects behaves as it always has.
    /// <see cref="RulesContext.Empty"/> holds no effect definitions, so on a state that holds effect instances a
    /// command that raises a rule event (a move raises <c>unitMoved</c>) throws <see cref="ArgumentException"/>. It holds
    /// no pools either, so on a state with pool spent counts a tick or a turn start refills nothing, and the result
    /// differs from the live run's with no error. Such states need the overload that takes the rules.
    /// </summary>
    public static StepResult Apply(GameState state, Command command) => Apply(state, command, RulesContext.Empty);

    /// <summary>
    /// Applies <paramref name="command"/>, then fires the triggers of the active effects that answer its events
    /// (MAIN 6.4; AB 14.4). <see cref="StepResult.Events"/> lists the events in dispatch phases, each phase followed by
    /// the trigger-produced events it fires, in firing order (M1-11 plan call 8):
    /// <list type="bullet">
    /// <item>an ordinary command: its own events, then each pass of the settle loop (a won fight's end, round 1's start,
    /// or a vacated turn's resumption);</item>
    /// <item>a move or a use out of combat that is not an opener: a tick (<see cref="ApplyTick"/>), each step dispatched
    /// in turn, then the settle passes;</item>
    /// <item>an opener (a use out of combat that affects another faction): combat start (<see cref="CombatStarted"/> and
    /// the held messages), then the use, then the settle passes.</item>
    /// </list>
    /// Dispatch stops once the run ends; the settle loop does not run while the run has ended or a decision is pending.
    /// A command that leaves an ability use suspended on an "ask me" decision dispatches nothing (M1-12a plan call 12;
    /// review finding C1): its events that can fire triggers (<see cref="Dispatchable"/>) join the frame's
    /// <see cref="AbilityFrame.Held"/>. The command that completes the use dispatches the held events, then its own, in
    /// one pass, and reports only its own events and what the pass fires; so a trigger answering a held event (an
    /// on-death trigger, say) shows its events in the answer's result, as an automatic policy would after the use.
    /// All phases share one firing counter, so the bound applies to the whole command. A consumer that needs phase
    /// boundaries reads the marker events (<see cref="CombatStarted"/>, <see cref="AbilityUsed"/>,
    /// <see cref="TurnStarted"/>, <see cref="CombatEnded"/>) rather than positions. A rejected command fires nothing.
    /// Throws <see cref="TriggerLoopException"/> if the command fires more than
    /// <see cref="Effects.Effects.MaxTriggerFirings"/> triggers.
    /// </summary>
    public static StepResult Apply(GameState state, Command command, RulesContext rules)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(rules);

        // After the run ends the core refuses every command (DH 10.1), checked before anything else (M1-10 plan call 3).
        if (state.Ended is not null)
        {
            return Reject(state, CommandError.RunEnded);
        }

        // While a decision is pending, the only valid command is an answer to it.
        if (state.Pending is not null && command is not AnswerDecisionCommand)
        {
            return Reject(state, CommandError.DecisionPending);
        }

        // An opener dispatches in phases of its own (M1-11 plan call 8).
        if (command is UseAbilityCommand opener && AbilityUse.Opens(rules, state, opener.User, opener.AbilityId, opener.Target))
        {
            return ApplyOpener(state, opener, rules);
        }

        // Out of combat, any other move or use is a tick, which dispatches step by step (M1-12a plan call 13).
        if (state.Combat is null && command is MoveCommand or UseAbilityCommand)
        {
            return ApplyTick(state, command, rules);
        }

        var result = command switch
        {
            MoveCommand move => ApplyMove(state, move, rules),
            EndTurnCommand end => ApplyEndTurn(state, end, rules),
            UseAbilityCommand use => ApplyUseAbility(state, use, rules),
            SetReactionPolicyCommand policy => ApplySetReactionPolicy(state, policy, rules),
            AnswerDecisionCommand answer => ApplyAnswer(state, answer, rules),
            _ => throw new NotSupportedException($"Unsupported command type {command.GetType().Name}."),
        };
        if (!result.Ok)
        {
            return result;
        }

        // A use still suspended holds what can fire triggers and dispatches nothing (M1-12a plan call 12). The held list is
        // the stored frame's (non-empty only when answering a suspended use) plus this command's.
        var held = state.Resolution?.Held ?? [];
        if (result.State.Resolution is { } frame)
        {
            var hold = result.State with { Resolution = frame with { Held = held.AddRange(Dispatchable(rules, result.Events)) } };
            return new StepResult(hold, result.Events, CommandError.None);
        }

        // One firing counter for every dispatch phase of the command (M1-11 plan call 8). A completed use dispatches its
        // held events first, then the command's own; the held events were reported when raised, so they are left out of
        // the result. With nothing held, fired or settled, the result is returned as the handler made it, so M0 behaviour
        // is unchanged.
        int firings = 0;
        var (next, events) = TriggerDispatch.Run(rules, result.State, held.IsEmpty ? result.Events : [.. held, .. result.Events], ref firings);
        if (!held.IsEmpty)
        {
            events = ImmutableArray.Create(events, held.Length, events.Length - held.Length);
        }

        (next, events) = Settle(rules, next, events, ref firings);
        return ReferenceEquals(next, result.State) && events == result.Events
            ? result
            : new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// The events of <paramref name="events"/> that raise at least one rule event under <paramref name="rules"/>
    /// (<see cref="RulesContext.EventsRaisedBy"/>), in order: the only ones that can fire triggers, and so what a suspended
    /// use holds (M1-12a plan call 12).
    /// </summary>
    internal static ImmutableArray<GameEvent> Dispatchable(RulesContext rules, ImmutableArray<GameEvent> events)
    {
        var dispatchable = ImmutableArray.CreateBuilder<GameEvent>();
        foreach (var e in events)
        {
            if (!rules.EventsRaisedBy(e).IsEmpty)
            {
                dispatchable.Add(e);
            }
        }

        return dispatchable.ToImmutable();
    }

    /// <summary>
    /// The settle loop (M1-11 plan call 8), run after an accepted command's events and their triggers: while the run has
    /// not ended and no decision is pending, end a won fight (<see cref="CombatEnd.EndIfWon"/>); otherwise start round 1
    /// when the fight is held before it (<see cref="Turns.BeginRound1"/>); otherwise start a vacated turn
    /// (<see cref="Turns.ResumeIfVacated(RulesContext, GameState)"/>). All share the command's firing counter. A won
    /// fight's pass is dispatched here; a turn start dispatches inside its own step (M1-12a plan call 9; owner ruling 4),
    /// so its pass comes back already dispatched and is not dispatched again. The loop stops at the first pass that
    /// emits nothing.
    /// </summary>
    private static (GameState State, ImmutableArray<GameEvent> Events) Settle(
        RulesContext rules, GameState s, ImmutableArray<GameEvent> events, ref int firings)
    {
        while (s.Ended is null && s.Pending is null)
        {
            var (pass, next) = CombatEnd.EndIfWon(rules, s);
            if (!pass.IsEmpty)
            {
                (s, pass) = TriggerDispatch.Run(rules, next, pass, ref firings);
                events = events.AddRange(pass);
                continue;
            }

            (pass, next) = s.Combat is { BeforeRound1: true }
                ? Turns.BeginRound1(rules, s, ref firings)
                : Turns.ResumeIfVacated(rules, s, ref firings);
            if (pass.IsEmpty)
            {
                break;
            }

            s = next;
            events = events.AddRange(pass);
        }

        return (s, events);
    }

    /// <summary>
    /// An opener (M1-11 plan calls 4 to 8; owner ruling 2; M1-10 rulings record 4.1 and 5): a use out of combat that
    /// affects a unit of another faction. It is validated on the combat-start view (<see cref="Turns.CombatStartView"/>:
    /// the combat-start condition step, then step 4; M1-12a plan call 14), and a refusal returns the state it was given
    /// with no events: no fight, no tick, no draw. Then, in phases, each dispatched on the command's one
    /// firing counter:
    /// <list type="number">
    /// <item><see cref="Turns.BeginCombat"/> (<see cref="CombatStarted"/>, then the held condition messages), dispatched, so
    /// <c>combatStarted</c> triggers see the state before the opener. If the run ended there, the events so far are
    /// returned (<see cref="RunEnded"/> last). If the striker is gone or can no longer pay the cost
    /// (<see cref="AbilityUse.Payable"/>), the opener is dropped: no <see cref="AbilityUsed"/>, no cost, no mark.</item>
    /// <item>Otherwise the opener resolves (<see cref="Resolution.Use"/>): the cost is paid on the step-4 budget, the
    /// striker is marked with it, the targets lock and resolve; then dispatched. A window that asks the party (plan call
    /// 12) suspends it on a reaction decision: its events are held in <see cref="AbilityFrame.Held"/> and not dispatched
    /// (M1-12a plan call 12), the marker stays set, the settle loop does nothing, and the answer resolves the rest,
    /// dispatches the held events and its own, and then starts round 1. Combat start's phase is never held.</item>
    /// <item>The settle loop: the win check (<see cref="CombatEnded"/>, and no turn event), else round 1 starts with
    /// slot 0's <see cref="TurnStarted"/>.</item>
    /// </list>
    /// There is no tick and no tick reset (AB 6.8).
    /// </summary>
    private static StepResult ApplyOpener(GameState state, UseAbilityCommand use, RulesContext rules)
    {
        var view = AbilityUse.View(rules, state, use.User, use.AbilityId, use.Target);
        var error = AbilityUse.Targetable(rules, view, use.User, use.AbilityId, use.Target);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        int firings = 0;
        var ability = rules.Ability(use.AbilityId);
        var (begun, started) = Turns.BeginCombat(rules, state);
        var (s, events) = TriggerDispatch.Run(rules, started, begun, ref firings);
        if (s.Ended is not null)
        {
            return new StepResult(s, events, CommandError.None);
        }

        if (s.FindUnit(use.User) is not null && AbilityUse.Payable(rules, s, use.User, ability) == CommandError.None)
        {
            var (used, resolved) = Resolution.Use(rules, s, use.User, ability, use.Target, opener: true);
            if (resolved.Resolution is { } frame)
            {
                // Suspended: the use's events are held, not dispatched (M1-12a plan call 12); combat start's were above.
                s = resolved with { Resolution = frame with { Held = Dispatchable(rules, used) } };
                events = events.AddRange(used);
            }
            else
            {
                var (dispatched, all) = TriggerDispatch.Run(rules, resolved, used, ref firings);
                s = dispatched;
                events = events.AddRange(all);
            }
        }

        (s, events) = Settle(rules, s, events, ref firings);
        return new StepResult(s, events, CommandError.None);
    }

    /// <summary>
    /// An ability use in combat (AB 14.3; M1-11 plan calls 3 to 5 and 10): validated and resolved on the stored state,
    /// with no tick. A refusal returns the state it was given, with no events. Out of combat a use is an opener
    /// (<see cref="ApplyOpener"/>) or a tick (<see cref="ApplyTick"/>).
    /// </summary>
    private static StepResult ApplyUseAbility(GameState state, UseAbilityCommand use, RulesContext rules)
    {
        var error = CheckUse(rules, state, use);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        var (events, next) = Resolution.Use(rules, state, use.User, rules.Ability(use.AbilityId), use.Target);
        return new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// An out-of-combat tick (AB 6.8; CO 5.5; M1-12a plan call 13): a move, or a use that opens no fight, takes one tick,
    /// on one firing counter, in this order:
    /// <list type="number">
    /// <item>the command is validated on the stored state (a use on its tick view, <see cref="AbilityUse.TickView"/>); a
    /// refusal returns the state it was given, with no events: no tick step, no reset, no draw;</item>
    /// <item><see cref="GameState.Phase"/> + 1 into the shared turn-start step; item 1 is the tick's resets
    /// (<see cref="TickResets"/>: every budget fresh, every turn pool refilled); then every unit's turn-start step in id
    /// order (<see cref="Clock.TurnStart"/>: no <see cref="TurnStarted"/>, no lost-turn check, the tick batch floored at
    /// 1 HP for every unit), each dispatching its own events;</item>
    /// <item><see cref="GameState.Phase"/> + 1 into the step phase, every unit's Act phase (CO 5.2); the command is
    /// validated again on that state, and refused after all (the stored state returned, with no events) if that fails;
    /// then it resolves there;</item>
    /// <item>the command's events are dispatched;</item>
    /// <item><see cref="GameState.Phase"/> + 1 into the shared turn-end step; every unit's turn-end step in id order
    /// (<see cref="Clock.TurnEnd"/>), each dispatched, then the windows whose source is gone end
    /// (<see cref="Clock.TickEnd"/>), dispatched;</item>
    /// <item><see cref="GameState.Tick"/> + 1, the clock leaves its step (<see cref="ClockStep.None"/>), and
    /// <see cref="TickAdvanced"/>;</item>
    /// <item>a step onto a shrine raises its <see cref="DecisionRequested"/> (three shrine draws) after
    /// <see cref="TickAdvanced"/>; these last events are dispatched too;</item>
    /// <item>the settle loop.</item>
    /// </list>
    /// Once the run ends, nothing later runs. A command whose dispatch starts a fight skips steps 5 to 7 (none can in
    /// M1).
    /// </summary>
    private static StepResult ApplyTick(GameState state, Command command, RulesContext rules)
    {
        var error = CheckTick(rules, state, command);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        // 2. Every unit's turn-start step, after the tick's resets (item 1).
        int firings = 0;
        var events = ImmutableArray.CreateBuilder<GameEvent>();
        var s = TickResets(rules, state with { Phase = checked(state.Phase + 1), Step = ClockStep.TurnStart });
        foreach (var id in UnitIds(s))
        {
            if (s.Ended is null && s.FindUnit(id) is not null)
            {
                var (started, next) = Clock.TurnStart(rules, s, id, ref firings);
                events.AddRange(started);
                s = next;
            }
        }

        if (s.Ended is not null)
        {
            return new StepResult(s, events.ToImmutable(), CommandError.None);
        }

        // 3. The step phase: validated again on the state after the turn-start steps, then resolved there.
        s = s with { Phase = checked(s.Phase + 1), Step = ClockStep.Act };
        error = CheckTick(rules, s, command);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        GridPos? shrine = null;
        ImmutableArray<GameEvent> own;
        if (command is MoveCommand move)
        {
            int index = IndexOfUnit(s, move.Unit);
            var unit = s.Units[index];
            var to = unit.Pos.Step(move.Dir);
            own = [new UnitMoved(unit.Id, unit.Pos, to)];
            s = s with { Units = s.Units.SetItem(index, unit with { Pos = to }) };
            shrine = s.Map[to] == Tile.Shrine ? to : null;
        }
        else
        {
            var use = (UseAbilityCommand)command;
            (own, s) = Resolution.Use(rules, s, use.User, rules.Ability(use.AbilityId), use.Target);
            if (s.Resolution is not null)
            {
                // A use out of combat draws nothing (it is refused NotInCombat if it would), so no reaction window opens.
                throw new InvalidOperationException("An out-of-combat use was suspended on a reaction decision.");
            }
        }

        // 4. The command's events.
        var (afterCommand, dispatched) = TriggerDispatch.Run(rules, s, own, ref firings);
        s = afterCommand;
        events.AddRange(dispatched);

        // 5. Every unit's turn-end step, then the windows whose source is gone.
        if (s.Ended is null && s.Combat is null)
        {
            s = s with { Phase = checked(s.Phase + 1), Step = ClockStep.TurnEnd };
            foreach (var id in UnitIds(s))
            {
                if (s.Ended is null && s.FindUnit(id) is not null)
                {
                    var (ended, next) = Clock.TurnEnd(rules, s, id);
                    (s, ended) = TriggerDispatch.Run(rules, next, ended, ref firings);
                    events.AddRange(ended);
                }
            }

            if (s.Ended is null)
            {
                var (gone, next) = Clock.TickEnd(rules, s);
                (s, gone) = TriggerDispatch.Run(rules, next, gone, ref firings);
                events.AddRange(gone);
            }

            // 6. The tick advances; 7. a shrine's decision follows it.
            if (s.Ended is null)
            {
                // Checked: at the limit the command fails loudly rather than wrap to a state no save can hold.
                long tick = checked(state.Tick + 1);
                s = s with { Tick = tick, Step = ClockStep.None };
                var tail = ImmutableArray.CreateBuilder<GameEvent>();
                tail.Add(new TickAdvanced(tick));
                if (shrine is { } source && s.FindUnit(((MoveCommand)command).Unit) is { } mover)
                {
                    var (decision, next) = RequestShrine(s, mover.Id, source);
                    s = next;
                    tail.Add(new DecisionRequested(decision));
                }

                var (afterTail, raised) = TriggerDispatch.Run(rules, s, tail.ToImmutable(), ref firings);
                s = afterTail;
                events.AddRange(raised);
            }
        }

        // 8. The settle loop.
        var (settled, all) = Settle(rules, s, events.ToImmutable(), ref firings);
        return new StepResult(settled, all, CommandError.None);
    }

    // A tick's command check (plan call 13): a move's checks, or a use's on its view (the tick view, or, should the use
    // open a fight, the combat-start view).
    private static CommandError CheckTick(RulesContext rules, GameState s, Command command) => command switch
    {
        MoveCommand move => CheckMove(rules, s, move),
        UseAbilityCommand use => CheckUse(rules, s, use),
        _ => throw new NotSupportedException($"Unsupported tick command type {command.GetType().Name}."),
    };

    private static CommandError CheckUse(RulesContext rules, GameState s, UseAbilityCommand use) =>
        AbilityUse.Targetable(rules, AbilityUse.View(rules, s, use.User, use.AbilityId, use.Target), use.User, use.AbilityId, use.Target);

    // Every unit's id, in id order (the units are kept sorted by id).
    private static ImmutableArray<UnitId> UnitIds(GameState s)
    {
        var ids = ImmutableArray.CreateBuilder<UnitId>(s.Units.Length);
        foreach (var u in s.Units)
        {
            ids.Add(u.Id);
        }

        return ids.MoveToImmutable();
    }

    /// <summary>
    /// Stores a reaction policy override (M1-11 plan call 13; <see cref="Reactions.CheckSetting"/>): no tick, no budget,
    /// no event, in or out of combat and on any unit's turn.
    /// </summary>
    private static StepResult ApplySetReactionPolicy(GameState state, SetReactionPolicyCommand set, RulesContext rules)
    {
        if (!Enum.IsDefined(set.Mode))
        {
            throw new ArgumentOutOfRangeException(nameof(set), set.Mode, "Unknown reaction mode.");
        }

        var error = Reactions.CheckSetting(rules, state, set.Unit, set.AbilityId, set.Mode);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        int index = IndexOfUnit(state, set.Unit);
        var unit = state.Units[index];
        var next = state with
        {
            Units = state.Units.SetItem(index, unit with { Reactions = Reactions.With(unit.Reactions, set.AbilityId, set.Mode) }),
        };
        return new StepResult(next, ImmutableArray<GameEvent>.Empty, CommandError.None);
    }

    /// <summary>
    /// A one-square move in combat (plan call 8): only the current unit may move (no unit while the turn is vacated or
    /// the fight is held before round 1, M1-11 plan call 7), and only with movement left (checked before the square); the
    /// move spends 1 movement and does not tick (ticks are out-of-combat time, CO 5.5). A step onto a shrine raises its
    /// decision. Out of combat a move is a tick (<see cref="ApplyTick"/>).
    /// </summary>
    private static StepResult ApplyMove(GameState state, MoveCommand move, RulesContext rules)
    {
        var error = CheckMove(rules, state, move);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        int index = IndexOfUnit(state, move.Unit);
        var unit = state.Units[index];
        var from = unit.Pos;
        var to = from.Step(move.Dir);

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new UnitMoved(unit.Id, from, to));
        var budget = unit.Budget with { MovementUsed = checked(unit.Budget.MovementUsed + 1) };
        var next = state with { Units = state.Units.SetItem(index, unit with { Pos = to, Budget = budget }) };
        if (state.Map[to] == Tile.Shrine)
        {
            var (decision, requested) = RequestShrine(next, unit.Id, to);
            next = requested;
            events.Add(new DecisionRequested(decision));
        }

        return new StepResult(next, events.ToImmutable(), CommandError.None);
    }

    /// <summary>
    /// A move's checks, in order: the direction (an undefined one throws <see cref="ArgumentOutOfRangeException"/>,
    /// before anything else so the outcome does not depend on the rest of the command); the unit
    /// (<see cref="CommandError.UnknownUnit"/>); in combat, the turn (<see cref="CommandError.NotYourTurn"/>) and the
    /// movement left (<see cref="CommandError.NoMovementLeft"/>); then the square (<see cref="CheckStep"/>).
    /// </summary>
    private static CommandError CheckMove(RulesContext rules, GameState state, MoveCommand move)
    {
        if (!Enum.IsDefined(move.Dir))
        {
            throw new ArgumentOutOfRangeException(nameof(move), move.Dir, "Unknown direction.");
        }

        int index = IndexOfUnit(state, move.Unit);
        if (index < 0)
        {
            return CommandError.UnknownUnit;
        }

        var unit = state.Units[index];
        if (state.Combat is { } combat)
        {
            // A vacated turn is held by no one until it resumes, and before round 1 no one holds the turn.
            if (combat.TurnVacated || combat.BeforeRound1 || combat.CurrentUnit != unit.Id)
            {
                return CommandError.NotYourTurn;
            }

            if (Turns.MovementLeft(rules, state, unit.Id) < 1)
            {
                return CommandError.NoMovementLeft;
            }
        }

        return CheckStep(state, unit, move.Dir);
    }

    // A shrine at `source` offers `unit` its boons: three draws on the shrine stream, and the decision pending.
    private static (PendingDecision Decision, GameState Next) RequestShrine(GameState s, UnitId unit, GridPos source)
    {
        var (options, shrine) = DrawBoons(s.Rng.Shrine);
        var decision = new PendingDecision(new DecisionId(s.NextDecisionId), unit, DecisionKind.ShrineBoon, options, source);
        return (decision, s with
        {
            Rng = s.Rng with { Shrine = shrine },
            Pending = decision,
            NextDecisionId = checked(s.NextDecisionId + 1),
        });
    }

    // AB 6.8, at the start of each tick, as its turn-start item 1 (M1-12a plan call 13): every unit's turn budget becomes
    // fresh (the camp flag included) and every turn pool refills.
    internal static GameState TickResets(RulesContext rules, GameState s) =>
        Pools.RefillAll(rules, Turns.FreshBudgets(s), RefillTag.Turn);

    /// <summary>
    /// Ends the current unit's turn (M1-10 plan call 9): refused out of combat, for an unknown unit, and out of turn
    /// (a vacated turn and a fight held before round 1 included); otherwise <see cref="Turns.EndTurn"/>, whose events
    /// <see cref="Apply"/> dispatches before the settle loop starts the next turn (M1-12a plan call 10).
    /// </summary>
    private static StepResult ApplyEndTurn(GameState state, EndTurnCommand end, RulesContext rules)
    {
        var combat = state.Combat;
        if (combat is null)
        {
            return Reject(state, CommandError.NotInCombat);
        }

        if (IndexOfUnit(state, end.Unit) < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        if (combat.TurnVacated || combat.BeforeRound1 || combat.CurrentUnit != end.Unit)
        {
            return Reject(state, CommandError.NotYourTurn);
        }

        // The turn ends here; Apply dispatches its events, then the settle loop ends a won fight or starts the next turn
        // (M1-12a plan call 10).
        var (events, next) = Turns.EndTurn(rules, state);
        return new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// Whether <paramref name="u"/> may step one square in direction <paramref name="d"/> from
    /// where it stands: <see cref="CommandError.None"/> if so, otherwise the reason it may not.
    /// Shared by <see cref="MoveCommand"/> and the path query so both obey the same rules.
    /// </summary>
    internal static CommandError CheckStep(GameState s, Unit u, Direction d)
    {
        var from = u.Pos;
        var to = from.Step(d);

        if (!s.Map.InBounds(to))
        {
            return CommandError.OutOfBounds;
        }

        if (!s.Map.IsWalkable(to))
        {
            return CommandError.Blocked;
        }

        // No corner cutting: a diagonal step needs both squares it passes between to be open.
        if (d.IsDiagonal()
            && (!s.Map.IsWalkable(new GridPos(to.X, from.Y))
                || !s.Map.IsWalkable(new GridPos(from.X, to.Y))))
        {
            return CommandError.CornerCut;
        }

        if (s.UnitAt(to) is not null)
        {
            return CommandError.Occupied;
        }

        return CommandError.None;
    }

    private static StepResult ApplyAnswer(GameState state, AnswerDecisionCommand answer, RulesContext rules)
    {
        var pending = state.Pending;
        if (pending is null)
        {
            return Reject(state, CommandError.NoDecisionPending);
        }

        if (answer.Decision != pending.Id)
        {
            return Reject(state, CommandError.WrongDecision);
        }

        if (answer.OptionIndex < 0 || answer.OptionIndex >= pending.Options.Length)
        {
            return Reject(state, CommandError.InvalidOption);
        }

        int index = IndexOfUnit(state, pending.Unit);
        if (index < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        return pending.Kind switch
        {
            DecisionKind.ShrineBoon => ResolveShrineBoon(state, pending, index, answer.OptionIndex),
            DecisionKind.Reaction => ResolveReaction(state, pending, answer.OptionIndex, rules),
            _ => throw new NotSupportedException($"Unsupported decision kind {pending.Kind}."),
        };
    }

    /// <summary>
    /// Answers an "ask me" reaction (M1-11 plan call 12): <see cref="DecisionResolved"/> with the chosen option, then the
    /// suspended use continues (<see cref="Resolution.Continue"/>): take fires the reaction, decline walks on to the next
    /// candidate, which may ask again. Nothing is drawn before the window closes and no tick passes; the caller then
    /// dispatches the events and runs the settle loop, which starts round 1 for a suspended opener.
    /// </summary>
    private static StepResult ResolveReaction(GameState state, PendingDecision pending, int optionIndex, RulesContext rules)
    {
        int option = pending.Options[optionIndex];
        var (events, next) = Resolution.Continue(rules, state, option == Resolution.Take);
        return new StepResult(
            next,
            [new DecisionResolved(pending.Id, pending.Unit, option), .. events],
            CommandError.None);
    }

    /// <summary>The unit takes the chosen boon and the shrine it stood on becomes plain floor.</summary>
    private static StepResult ResolveShrineBoon(GameState state, PendingDecision pending, int index, int optionIndex)
    {
        var unit = state.Units[index];
        int boon = pending.Options[optionIndex];
        var next = state with
        {
            Map = state.Map.With(pending.Source, Tile.Floor),
            Units = state.Units.SetItem(index, unit with { Boons = unit.Boons.Add(boon) }),
            Pending = null,
        };
        return new StepResult(
            next,
            ImmutableArray.Create<GameEvent>(
                new DecisionResolved(pending.Id, unit.Id, boon),
                new TileChanged(pending.Source, Tile.Floor)),
            CommandError.None);
    }

    /// <summary>
    /// Draws <see cref="ShrineBoonChoices"/> distinct boon ids from the shrine stream: a partial
    /// Fisher-Yates shuffle of <c>[0, BoonCount)</c> that makes exactly one draw per option, so the
    /// stream advances by the same count under every seed.
    /// </summary>
    private static (ImmutableArray<int> Options, DrawStream Next) DrawBoons(DrawStream shrine)
    {
        var boons = new int[BoonCount];
        for (int i = 0; i < BoonCount; i++)
        {
            boons[i] = i;
        }

        for (int i = 0; i < ShrineBoonChoices; i++)
        {
            (ulong offset, shrine) = shrine.NextBelow(checked((ulong)(BoonCount - i)));
            int j = checked(i + (int)offset);
            (boons[i], boons[j]) = (boons[j], boons[i]);
        }

        return (ImmutableArray.Create(boons, 0, ShrineBoonChoices), shrine);
    }

    private static int IndexOfUnit(GameState state, UnitId id)
    {
        for (int i = 0; i < state.Units.Length; i++)
        {
            if (state.Units[i].Id == id)
            {
                return i;
            }
        }

        return -1;
    }

    private static StepResult Reject(GameState state, CommandError error) =>
        new(state, ImmutableArray<GameEvent>.Empty, error);
}
