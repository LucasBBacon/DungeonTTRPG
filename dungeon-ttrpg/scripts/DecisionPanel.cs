using Dungeon.Core;
using Godot;

namespace DungeonTTRPG;

/// <summary>
/// Shows the options of a pending decision, one button each, and reports which one was picked,
/// by button or by the <c>option_1</c>..<c>option_3</c> actions. It does not answer the decision itself.
/// </summary>
public partial class DecisionPanel : Control
{
    private static readonly StringName[] OptionActions = ["option_1", "option_2", "option_3"];

    private Container _buttons = null!;

    /// <summary>Emitted with the zero-based index of the option the player picked.</summary>
    [Signal]
    public delegate void OptionChosenEventHandler(int index);

    public override void _Ready()
    {
        _buttons = GetNode<Container>("Layout/Buttons");
        Visible = false;
    }

    /// <summary>Shows one button per option of <paramref name="decision"/>, labelled with the boon id.</summary>
    public void Open(PendingDecision decision)
    {
        Clear();
        for (int i = 0; i < decision.Options.Length; i++)
        {
            int index = i;
            var button = new Button { Text = $"Boon {decision.Options[i]}", FocusMode = FocusModeEnum.None };
            button.Pressed += () => EmitSignal(SignalName.OptionChosen, index);
            _buttons.AddChild(button);
        }

        Visible = true;
    }

    public void Close()
    {
        Visible = false;
        Clear();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!Visible)
        {
            return;
        }

        for (int i = 0; i < OptionActions.Length; i++)
        {
            if (@event.IsActionPressed(OptionActions[i]))
            {
                EmitSignal(SignalName.OptionChosen, i);
                GetViewport().SetInputAsHandled();
                return;
            }
        }
    }

    private void Clear()
    {
        foreach (var child in _buttons.GetChildren())
        {
            _buttons.RemoveChild(child);
            child.QueueFree();
        }
    }
}
