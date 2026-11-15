using System.Collections.Generic;
using Dungeon.Core;
using Godot;
using CoreEngine = Dungeon.Core.Engine;

namespace DungeonTTRPG;

/// <summary>
/// Owns the current game state. Turns input into commands for the rules core and plays the
/// events it returns, in order, one at a time. It never changes the state itself and decides
/// no rule: what is legal comes from <c>Engine.Apply</c> and <c>Queries</c>.
/// </summary>
public partial class GameController : Node3D
{
    private readonly Queue<GameEvent> _events = new();

    private GameState _state = DemoLevel.CreateState();
    private bool _playing;

    private BoardView _board = null!;
    private UnitView _hero = null!;
    private IsoCamera _camera = null!;
    private DecisionPanel _panel = null!;

    public override void _Ready()
    {
        _board = GetNode<BoardView>("BoardView");
        _hero = GetNode<UnitView>("UnitView");
        _camera = GetNode<IsoCamera>("IsoCamera");
        _panel = GetNode<DecisionPanel>("UI/DecisionPanel");

        _board.Build(_state.Map);
        _camera.Frame(_state.Map.Width, _state.Map.Height);
        _hero.Place(_state.FindUnit(DemoLevel.Hero)!.Pos);
        _hero.MoveFinished += PlayNext;
        _panel.OptionChosen += Answer;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // The action may be rebound to a key: then the target is whatever the pointer is over.
        if (@event.IsActionPressed("select"))
        {
            WalkTo(@event is InputEventMouse mouse ? mouse.Position : GetViewport().GetMousePosition());
        }
    }

    /// <summary>True while input must be ignored: events are still playing or a decision is pending.</summary>
    private bool Busy => Animating || _state.Pending is not null;

    /// <summary>True while events from an accepted command are still being shown.</summary>
    private bool Animating => _playing || _events.Count > 0;

    private void WalkTo(Vector2 screen)
    {
        if (Busy)
        {
            return;
        }

        var target = GridCoords.Pick(_camera, screen, _board.DrawnHeight, BoardView.MaxDrawnHeight);
        if (target is null)
        {
            return;
        }

        var path = Queries.FindPath(_state, DemoLevel.Hero, target.Value);
        if (path is null)
        {
            return;
        }

        foreach (var dir in path.Value)
        {
            var error = Submit(new MoveCommand(DemoLevel.Hero, dir));
            if (error != CommandError.None)
            {
                // The path came from the core's own query, so this means query and rules disagree.
                GD.PushWarning($"Move {dir} rejected mid-path: {error}");
                break;
            }

            if (_state.Pending is not null)
            {
                break;
            }
        }

        PlayNext();
    }

    private void Answer(int optionIndex)
    {
        if (Animating || _state.Pending is not { } pending)
        {
            return;
        }

        // An option key outside the offered range is rejected by the core and quietly ignored.
        if (Submit(new AnswerDecisionCommand(pending.Id, optionIndex)) == CommandError.None)
        {
            PlayNext();
        }
    }

    /// <summary>
    /// Applies a command. If the core accepts it, adopts the new state and queues its events.
    /// Returns the core's verdict: <see cref="CommandError.None"/> when accepted.
    /// </summary>
    private CommandError Submit(Command command)
    {
        var result = CoreEngine.Apply(_state, command);
        if (!result.Ok)
        {
            return result.Error;
        }

        _state = result.State;
        foreach (var e in result.Events)
        {
            _events.Enqueue(e);
        }

        return CommandError.None;
    }

    /// <summary>Plays queued events until one starts an animation; its end signal resumes playback.</summary>
    private void PlayNext()
    {
        _playing = true;
        while (_events.Count > 0)
        {
            if (Play(_events.Dequeue()))
            {
                return;
            }
        }

        _playing = false;
    }

    /// <summary>Shows one event. Returns true if it started an animation that must end before the next event.</summary>
    private bool Play(GameEvent e)
    {
        switch (e)
        {
            case UnitMoved moved when moved.Unit == DemoLevel.Hero:
                _hero.MoveTo(moved.To);
                return true;
            case DecisionRequested requested:
                _panel.Open(requested.Decision);
                return false;
            case DecisionResolved:
                _panel.Close();
                return false;
            case TileChanged changed:
                _board.SetTile(changed.Pos, changed.Tile);
                return false;
            case TickAdvanced:
                return false;   // the clock is not shown in M0
            default:
                GD.PushWarning($"No display for event {e}");
                return false;
        }
    }
}
