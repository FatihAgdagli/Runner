using UnityEngine;

public class PlayerController : Controller
{
    
    private InputHandler inputHandler;

    private float movementZ = 0f;
    private float movementX = 0f;

    override protected void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    override protected void FixedUpdate()
    {
        base .FixedUpdate();
        if (Alive && !GameManager.Instance.LevelFinished)
        {
            GetControllerInput();
            Move();
        }
        else if(GameManager.Instance.LevelFinished)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Move()
    {
        // Wait for the player input to start the game
        if (movementX != 0) GameManager.Instance.StartTheGame();

        //Move.Z =1 for move foraward the player continuously
        if (GameManager.Instance.Started) movementZ = 1f;


        Vector3 movement = new Vector3(movementX, 0, movementZ);
        if (onRotatingPlatform) movement -= Vector3.right * rotatingPlatformEfect;
        movement *= speed;
        movement = Vector3.ClampMagnitude(movement, speed);

        if (isGrounded) rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
    private void GetControllerInput()
    {
        movementX = inputHandler.GetHorizontalInput();
        movementZ = 0f;      
    }

    // Called by Rotation Platform
}
