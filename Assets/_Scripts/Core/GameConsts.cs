using UnityEngine;

public enum MovementStatus
{
    Idle,
    Walk,
    Lift,
    Push,
    Run,
    Die
}

public enum MovementDirection
{
    Up,
    Left,
    Right,
    Down
}

public struct InputStrings
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
    public const string Fire1 = "Fire1";
    public const string Fire3 = "Fire3";
}

public class AnimationTriggers
{
    public static readonly int[,] TriggerTable =
    {
        { Animator.StringToHash("idle_up"),  Animator.StringToHash("idle_side"),  Animator.StringToHash("idle_down") },
        { Animator.StringToHash("walk_up"),  Animator.StringToHash("walk_side"),  Animator.StringToHash("walk_down") },
        { Animator.StringToHash("lift_up"),  Animator.StringToHash("lift_side"),  Animator.StringToHash("lift_down") },
        { Animator.StringToHash("push_up"),  Animator.StringToHash("push_side"),  Animator.StringToHash("push_down") },
        { Animator.StringToHash("run_up"),   Animator.StringToHash("run_side"),   Animator.StringToHash("run_down") }
    };

    public static readonly int die = Animator.StringToHash("die");
}