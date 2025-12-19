using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    [Header("Configs")]
    [SerializeField] private float speed = 2;

    private MovementStatus currentMovement;
    private MovementDirection currentDirection;

    private Vector2 currentInput = Vector2.zero;
    private bool isRunning = false;
    private bool isUp = false;
    private bool isDown = false;
    private bool isLeft = false;
    private bool isRight = false;

    private bool sendMovementChange = false;

    private void Start()
    {
        PlayerController.OnDeath += DisableMovement;
    }

    private void OnDestroy()
    {
        PlayerController.OnDeath -= DisableMovement;
    }

    private void DisableMovement()
    {
        enabled = false;
    }

    private void Update()
    {
        if (ConversationSystem.instance.movementDisabled)
            return;

        sendMovementChange = false;

        ReadInputs();
        SetAnimationStatus();

        if (sendMovementChange)
        {
            SetSpriteAnimation_Movement(currentMovement, currentDirection);
            sendMovementChange = false;
        }

        rb.linearVelocity = speed * (isRunning ? 2 : 1) * currentInput;
    }

    private void ReadInputs()
    {
        isRunning = Input.GetButton(InputStrings.Fire3);

        currentInput.x = Input.GetAxisRaw(InputStrings.Horizontal);
        currentInput.y = Input.GetAxisRaw(InputStrings.Vertical);

        isUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        isDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        isLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        isRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private void SetAnimationStatus()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentDirection = MovementDirection.Up;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentDirection = MovementDirection.Down;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = MovementDirection.Left;
            sendMovementChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = MovementDirection.Right;
            sendMovementChange = true;
        }

        //
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            sendMovementChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            sendMovementChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            sendMovementChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            sendMovementChange = true;
        }

        if (sendMovementChange)
        {
            currentMovement = isRunning ? MovementStatus.Run : MovementStatus.Walk;

            if (isUp && !isDown && !isLeft && !isRight)
            {
                currentDirection = MovementDirection.Up;
            }
            else if (!isUp && isDown && !isLeft && !isRight)
            {
                currentDirection = MovementDirection.Down;
            }
            else if (!isUp && !isDown && isLeft && !isRight)
            {
                currentDirection = MovementDirection.Left;
            }
            else if (!isUp && !isDown && !isLeft && isRight)
            {
                currentDirection = MovementDirection.Right;
            }
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