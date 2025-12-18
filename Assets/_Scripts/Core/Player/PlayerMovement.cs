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
    private MovementStatus currentMovement;

    private enum MovementDirection
    {
        Up,
        Left,
        Right,
        Down
    }
    private MovementDirection currentDirection;

    private Vector2 currentInput = Vector2.zero;

    private void Update()
    {
        currentInput.x = Input.GetAxisRaw("Horizontal");
        currentInput.y = Input.GetAxisRaw("Vertical");

        if (currentInput.y > 0)
            currentDirection = MovementDirection.Up;
        else if (currentInput.y < 0)
            currentDirection = MovementDirection.Down;
        else if (currentInput.x < 0)
            currentDirection = MovementDirection.Left;
        else if (currentInput.x > 0)
            currentDirection = MovementDirection.Right;

        if (currentInput.magnitude == 0)
        {
            currentMovement = MovementStatus.Idle;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            currentMovement = MovementStatus.Walk;
        }

        //SetSpriteAnimation(currentMovement, currentDirection);
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