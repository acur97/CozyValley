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
    public static int GetPlayerAnimationInt(MovementStatus status, MovementDirection direction)
    {
        return TriggerTable[(int)status, direction switch
        {
            MovementDirection.Up => 0,
            MovementDirection.Down => 2,
            _ => 1
        }];
    }

    public static readonly int[,] TriggerTable =
    {
        { Animator.StringToHash("idle_up"),  idle_side,  Animator.StringToHash("idle_down") },
        { Animator.StringToHash("walk_up"),  walk_side,  Animator.StringToHash("walk_down") },
        { Animator.StringToHash("lift_up"),  Animator.StringToHash("lift_side"),  Animator.StringToHash("lift_down") },
        { Animator.StringToHash("push_up"),  Animator.StringToHash("push_side"),  Animator.StringToHash("push_down") },
        { Animator.StringToHash("run_up"),   Animator.StringToHash("run_side"),   Animator.StringToHash("run_down") }
    };

    public static readonly int idle_side = Animator.StringToHash("idle_side");
    public static readonly int walk_side = Animator.StringToHash("walk_side");
    public static readonly int hit_side = Animator.StringToHash("hit_side");
    public static readonly int die = Animator.StringToHash("die");
}

public enum EmoteType
{
    None,
    OpenBubble,
    NeedWater,
    Question,
    Noise,
    Exclamation,
    Heart,
    Sleep,
    Drop,
    FaceCute,
    Cancel,
    Think,
    Golden,
    Game,
    Music,
    FaceSad
}