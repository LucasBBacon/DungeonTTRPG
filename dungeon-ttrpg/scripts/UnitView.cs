using Dungeon.Core;
using Godot;

namespace DungeonTTRPG;

/// <summary>A placeholder capsule standing for one unit.</summary>
public partial class UnitView : MeshInstance3D
{
    private const float Height = 1.0f;
    private const float Radius = 0.3f;
    private const float StepSeconds = 0.15f;

    /// <summary>Emitted when the animation started by <see cref="MoveTo"/> has ended.</summary>
    [Signal]
    public delegate void MoveFinishedEventHandler();

    public override void _Ready()
    {
        Mesh = new CapsuleMesh
        {
            Height = Height,
            Radius = Radius,
            Material = new StandardMaterial3D { AlbedoColor = new Color(0.2f, 0.6f, 1.0f) },
        };
    }

    /// <summary>Puts the unit on <paramref name="pos"/> at once.</summary>
    public void Place(GridPos pos) => Position = Standing(pos);

    /// <summary>Slides the unit to <paramref name="pos"/>, then emits <see cref="MoveFinished"/>.</summary>
    public void MoveTo(GridPos pos)
    {
        var tween = CreateTween();
        tween.TweenProperty(this, Node3D.PropertyName.Position.ToString(), Standing(pos), StepSeconds);
        tween.Finished += () => EmitSignal(SignalName.MoveFinished);
    }

    private static Vector3 Standing(GridPos pos) => GridCoords.ToWorld(pos) + new Vector3(0, Height / 2, 0);
}
