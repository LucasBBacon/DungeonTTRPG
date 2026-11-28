using System;
using System.Collections.Immutable;
using Dungeon.Core.AbilityModel;
using Dungeon.Core.Combat;
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
    /// The command is valid only in combat; for an ability, a use out of combat that would draw (M1-11 plan call 5; D-23).
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
    /// (MAIN 6.4; AB 14.4). <see cref="StepResult.Events"/> holds the command's own events, then the trigger-produced
    /// events in firing order. A rejected command fires nothing. Throws <see cref="TriggerLoopException"/> if the
    /// command fires more than <see cref="Effects.Effects.MaxTriggerFirings"/> triggers.
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

        // One firing counter for every dispatch phase of the command (M1-11 plan call 8). With nothing fired and nothing
        // settled, the result is returned as the handler made it, so M0 behaviour is unchanged.
        int firings = 0;
        var (next, events) = TriggerDispatch.Run(rules, result.State, result.Events, ref firings);
        (next, events) = Settle(rules, next, events, ref firings);
        return ReferenceEquals(next, result.State) && events == result.Events
            ? result
            : new StepResult(next, events, CommandError.None);
    }

    /// <summary>
    /// The settle loop (M1-11 plan call 8), run after an accepted command's events and their triggers: while the run has
    /// not ended and no decision is pending, end a won fight (<see cref="CombatEnd.EndIfWon"/>); otherwise start round 1
    /// when the fight is held before it (<see cref="Turns.BeginRound1"/>); otherwise start a vacated turn
    /// (<see cref="Turns.ResumeIfVacated"/>). Each pass's events are dispatched in turn, on the command's firing counter;
    /// the loop stops at the first pass that emits nothing.
    /// </summary>
    private static (GameState State, ImmutableArray<GameEvent> Events) Settle(
        RulesContext rules, GameState s, ImmutableArray<GameEvent> events, ref int firings)
    {
        while (s.Ended is null && s.Pending is null)
        {
            var (pass, next) = CombatEnd.EndIfWon(rules, s);
            if (pass.IsEmpty)
            {
                (pass, next) = s.Combat is { BeforeRound1: true }
                    ? Turns.BeginRound1(rules, s)
                    : Turns.ResumeIfVacated(rules, s);
            }

            if (pass.IsEmpty)
            {
                break;
            }

            var (dispatched, all) = TriggerDispatch.Run(rules, next, pass, ref firings);
            s = dispatched;
            events = events.AddRange(all);
        }

        return (s, events);
    }

    /// <summary>
    /// An opener (M1-11 plan calls 4 to 8; owner ruling 2; M1-10 rulings record 4.1 and 5): a use out of combat that
    /// affects a unit of another faction. It is validated on combat start's step-4 view, and a refusal returns the state
    /// it was given with no events: no fight, no tick, no draw. Then, in phases, each dispatched on the command's one
    /// firing counter:
    /// <list type="number">
    /// <item><see cref="Turns.BeginCombat"/> (<see cref="CombatStarted"/>, then the held messages), dispatched, so
    /// <c>combatStarted</c> triggers see the state before the opener. If the run ended there, the events so far are
    /// returned (<see cref="RunEnded"/> last). If the striker is gone or can no longer pay the cost
    /// (<see cref="AbilityUse.Payable"/>), the opener is dropped: no <see cref="AbilityUsed"/>, no cost, no mark.</item>
    /// <item>Otherwise the opener resolves (<see cref="Resolution.Use"/>): the cost is paid on the step-4 budget, the
    /// striker is marked with it, the targets lock and resolve; then dispatched. A window that asks the party (plan call
    /// 12) suspends it on a reaction decision: the marker stays set, the settle loop does nothing, and the answer
    /// resolves the rest and then starts round 1.</item>
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
            var (dispatched, all) = TriggerDispatch.Run(rules, resolved, used, ref firings);
            s = dispatched;
            events = events.AddRange(all);
        }

        (s, events) = Settle(rules, s, events, ref firings);
        return new StepResult(s, events, CommandError.None);
    }

    /// <summary>
    /// An ability use that is not an opener (AB 14.3; M1-11 plan calls 3 to 5 and 10). In combat it is validated and
    /// resolved on the stored state and takes no tick. Out of combat (a use that affects no other faction) it runs as a
    /// step does: the tick's resets first (every budget fresh, every turn pool refilled; AB 6.8), then validation and
    /// resolution on that view, then <see cref="GameState.Tick"/> + 1 and <see cref="TickAdvanced"/> last. A refusal
    /// returns the state it was given, with no events: no reset, no tick, no draw.
    /// </summary>
    private static StepResult ApplyUseAbility(GameState state, UseAbilityCommand use, RulesContext rules)
    {
        var view = AbilityUse.View(rules, state, use.User, use.AbilityId, use.Target);
        var error = AbilityUse.Targetable(rules, view, use.User, use.AbilityId, use.Target);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        var (events, next) = Resolution.Use(rules, view, use.User, rules.Ability(use.AbilityId), use.Target);
        if (state.Combat is null && next.Ended is null)
        {
            // Checked: at the limit the use fails loudly rather than wrap to a state no save can hold.
            long tick = checked(state.Tick + 1);
            next = next with { Tick = tick };
            events = events.Add(new TickAdvanced(tick));
        }

        return new StepResult(next, events, CommandError.None);
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
    /// A one-square move. Out of combat it advances <see cref="GameState.Tick"/> and, as each tick does (AB 6.8; M1-10
    /// plan call 6), first gives every unit a fresh turn budget and refills every <see cref="RefillTag.Turn"/> pool, then
    /// makes the move (the tick's start steps come first, CO 5.5). In combat
    /// (plan call 8) only the current unit may move (no unit while the turn is vacated or the fight is held before round
    /// 1, M1-11 plan call 7), and only with movement left (checked before the square); the move spends 1 movement and
    /// does not tick.
    /// </summary>
    private static StepResult ApplyMove(GameState state, MoveCommand move, RulesContext rules)
    {
        // Checked before anything else so the outcome does not depend on the rest of the command.
        if (!Enum.IsDefined(move.Dir))
        {
            throw new ArgumentOutOfRangeException(nameof(move), move.Dir, "Unknown direction.");
        }

        int index = IndexOfUnit(state, move.Unit);
        if (index < 0)
        {
            return Reject(state, CommandError.UnknownUnit);
        }

        var unit = state.Units[index];
        var combat = state.Combat;
        if (combat is not null)
        {
            // A vacated turn is held by no one until it resumes, and before round 1 no one holds the turn.
            if (combat.TurnVacated || combat.BeforeRound1 || combat.CurrentUnit != unit.Id)
            {
                return Reject(state, CommandError.NotYourTurn);
            }

            if (Turns.MovementLeft(rules, state, unit.Id) < 1)
            {
                return Reject(state, CommandError.NoMovementLeft);
            }
        }

        var error = CheckStep(state, unit, move.Dir);
        if (error != CommandError.None)
        {
            return Reject(state, error);
        }

        var from = unit.Pos;
        var to = from.Step(move.Dir);

        var events = ImmutableArray.CreateBuilder<GameEvent>();
        events.Add(new UnitMoved(unit.Id, from, to));
        GameState next;
        if (combat is not null)
        {
            // In combat the move spends movement; ticks are out-of-combat time (CO 5.5).
            var budget = unit.Budget with { MovementUsed = checked(unit.Budget.MovementUsed + 1) };
            next = state with { Units = state.Units.SetItem(index, unit with { Pos = to, Budget = budget }) };
        }
        else
        {
            // Checked: at the limit the step fails loudly rather than wrap to a state no save can hold.
            long tick = checked(state.Tick + 1);
            next = TickResets(rules, state);
            next = next with
            {
                Units = next.Units.SetItem(index, next.Units[index] with { Pos = to }),
                Tick = tick,
            };
            events.Add(new TickAdvanced(tick));
        }

        if (state.Map[to] == Tile.Shrine)
        {
            var (options, shrine) = DrawBoons(state.Rng.Shrine);
            var decision = new PendingDecision(
                new DecisionId(state.NextDecisionId), unit.Id, DecisionKind.ShrineBoon, options, to);
            next = next with
            {
                Rng = state.Rng with { Shrine = shrine },
                Pending = decision,
                NextDecisionId = checked(state.NextDecisionId + 1),
            };
            events.Add(new DecisionRequested(decision));
        }

        return new StepResult(next, events.ToImmutable(), CommandError.None);
    }

    // AB 6.8, at the start of each tick, before the command's own step: every unit's turn budget becomes fresh (the
    // camp flag included) and every turn pool refills.
    internal static GameState TickResets(RulesContext rules, GameState s) =>
        Pools.RefillAll(rules, Turns.FreshBudgets(s), RefillTag.Turn);

    /// <summary>
    /// Ends the current unit's turn (M1-10 plan call 9): refused out of combat, for an unknown unit, and out of turn
    /// (a vacated turn and a fight held before round 1 included); otherwise <see cref="Turns.Advance"/>.
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

        var (events, next) = Turns.Advance(rules, state);
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
