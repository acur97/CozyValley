using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Animator anim;

    private MovementStatus currentMovement;
    private MovementDirection currentDirection;

    private Vector2 currentInput = Vector2.zero;
    private bool isRunning = false;

    private bool sendMovementChange = false;

    private void Update()
    {
        #region Movement
        currentInput.x = Input.GetAxisRaw(InputStrings.Horizontal);
        currentInput.y = Input.GetAxisRaw(InputStrings.Vertical);
        //currentInput.x = Input.GetAxis(InputStrings.Horizontal);
        //currentInput.y = Input.GetAxis(InputStrings.Vertical);
        isRunning = Input.GetButton(InputStrings.Fire3);

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentMovement = isRunning ? MovementStatus.Run : MovementStatus.Walk;
            currentDirection = MovementDirection.Up;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentMovement = isRunning ? MovementStatus.Run : MovementStatus.Walk;
            currentDirection = MovementDirection.Down;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentMovement = isRunning ? MovementStatus.Run : MovementStatus.Walk;
            currentDirection = MovementDirection.Left;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentMovement = isRunning ? MovementStatus.Run : MovementStatus.Walk;
            currentDirection = MovementDirection.Right;
            sendMovementChange = true;
        }

        if (Input.GetButtonDown(InputStrings.Fire3) && currentInput.magnitude != 0)
        {
            currentMovement = MovementStatus.Run;
            sendMovementChange = true;
        }
        else if (Input.GetButtonUp(InputStrings.Fire3) && currentInput.magnitude != 0)
        {
            currentMovement = MovementStatus.Walk;
            sendMovementChange = true;
        }
        else if (currentMovement != MovementStatus.Idle && currentInput.magnitude == 0)
        {
            currentMovement = MovementStatus.Idle;
            sendMovementChange = true;
        }

        if (sendMovementChange)
        {
            SetSpriteAnimation_Movement(currentMovement, currentDirection);
            sendMovementChange = false;
        }
        #endregion
    }

    private void SetSpriteAnimation_Movement(MovementStatus status, MovementDirection direction)
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