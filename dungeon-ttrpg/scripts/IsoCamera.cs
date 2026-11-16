using Godot;

namespace DungeonTTRPG;

/// <summary>
/// An orthographic camera at a fixed isometric pitch that orbits a target point in quarter turns.
/// </summary>
public partial class IsoCamera : Camera3D
{
    private const float PitchDegrees = 35.264f;
    private const float StartYawDegrees = 45f;
    private const float QuarterTurnDegrees = 90f;
    private const float TurnSeconds = 0.2f;
    private const float Distance = 30f;

    private Vector3 _target;
    private float _yawDegrees = StartYawDegrees;
    private float _targetYawDegrees = StartYawDegrees;
    private Tween? _tween;

    public override void _Ready()
    {
        Projection = ProjectionType.Orthogonal;
        Near = 0.1f;
        Far = Distance * 3;
        UpdateTransform();
    }

    /// <summary>Centres the view on a board of the given size lying on y = 0 from the world origin.</summary>
    public void Frame(int boardWidth, int boardDepth)
    {
        _target = new Vector3(boardWidth / 2f, 0, boardDepth / 2f);
        Size = (boardWidth + boardDepth) / 2f + 1;
        UpdateTransform();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("cam_rotate_left"))
        {
            Turn(-1);
        }
        else if (@event.IsActionPressed("cam_rotate_right"))
        {
            Turn(1);
        }
    }

    private void Turn(int quarterTurns)
    {
        _targetYawDegrees += quarterTurns * QuarterTurnDegrees;
        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenMethod(Callable.From<float>(SetYaw), _yawDegrees, _targetYawDegrees, TurnSeconds);
    }

    private void SetYaw(float degrees)
    {
        _yawDegrees = degrees;
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        var basis = new Basis(Vector3.Up, Mathf.DegToRad(_yawDegrees))
            * new Basis(Vector3.Right, Mathf.DegToRad(-PitchDegrees));
        Transform = new Transform3D(basis, _target + basis.Z * Distance);
    }
}
