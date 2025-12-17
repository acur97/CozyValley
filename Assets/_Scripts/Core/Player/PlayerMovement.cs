using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Animator anim;

    private enum MovementStatus
    {
        Idle,
        Walk,
        Lift,
        Push,
        Run,
        Die
    }

    private enum MovementDirection
    {
        Up,
        Left,
        Right,
        Down
    }

    private void SetSpriteAnimation(MovementStatus status, MovementDirection direction)
    {
        if (status == MovementStatus.Die)
            return;

        render.flipX = direction == MovementDirection.Left;

        anim.SetTrigger(AnimationTriggers.TriggerTable[(int)status, direction switch
        {
            MovementDirection.Up => 0,
            MovementDirection.Down => 2,
            _ => 1
        }]);
    }
}