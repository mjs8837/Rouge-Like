using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Jumping
}

public class Player : MonoBehaviour
{
    /// <summary>
    /// Camera that will follow the player as it moves
    /// </summary>
    [SerializeField] Camera followCamera;

    /// <summary>
    /// Constant scale at which camera rotation speed is multipled by
    /// </summary>
    private const float CAMERA_SPEED_SCALE = 2.0f;

    /// <summary>
    /// Constant scale at which walk speed is multiplied by
    /// </summary>
    private const float WALK_SPEED_SCALE = 0.05f;

    /// <summary>
    /// Constant scale at which sprint speed is multiplied by
    /// </summary>
    private const float SPRINT_SPEED_SCALE = 0.1f;

    /// <summary>
    /// Constant magnitude at which velocity is clamped at
    /// </summary>
    private const float MAX_VELOCITY = 3.0f;

    /// <summary>
    /// Constant scale at which jump height is multiplied by
    /// </summary>
    private const float JUMP_SCALE = 7.5f;

    /// <summary>
    /// Scale at which the player movement speed is multipled by
    /// </summary>
    private float speedScale;


    /// <summary>
    /// Scale at which gravity is applied to the player
    /// </summary>
    private float gravityScale = 0.5f;

    /// <summary>
    /// Tracking if the player is on the ground
    /// </summary>
    private bool onGround = true;

    /// <summary>
    /// Current player movement state
    /// </summary>
    public PlayerState currentPlayerState = PlayerState.Idle;

    /// <summary>
    /// Player rigidbody
    /// </summary>
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speedScale = WALK_SPEED_SCALE;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting delta x and y for mouse movement to control the camera
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float jumpInput = Input.GetAxis("Jump");

        ClampVelocity();
        MovementInput();

        // Setting the camera to follow the player
        followCamera.transform.position = transform.position;

        // Setting up camera rotation for the player
        followCamera.transform.eulerAngles += CAMERA_SPEED_SCALE * new Vector3(-mouseY, mouseX, 0.0f);
        transform.eulerAngles = new Vector3(0.0f, followCamera.transform.eulerAngles.y, 0.0f);

        if (!onGround)
        {
            Vector3 gravity = GameManager.GLOBAL_GRAVITY * gravityScale * transform.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
            currentPlayerState = PlayerState.Jumping;
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpInput * JUMP_SCALE, rb.velocity.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("TEST");
        }
    }

    /// <summary>
    /// Helper method to clamp velocity. Called every frame
    /// </summary>
    private void ClampVelocity()
    {
        // Clamping rigidbody velocity by applying a brake speed
        float speed = Vector3.Magnitude(rb.velocity);

        if (speed > MAX_VELOCITY)
        {
            float brakeSpeed = speed - MAX_VELOCITY;

            Vector3 normalizedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalizedVelocity * brakeSpeed;

            rb.AddForce(-brakeVelocity);
        }
    }

    /// <summary>
    /// Helper method to handle player input for movement. Called every frame
    /// </summary>
    private void MovementInput()
    {
        // Forward movement
        if (Input.GetKey(KeyCode.W)) 
        { rb.velocity += speedScale * new Vector3(followCamera.transform.forward.normalized.x, 0.0f, followCamera.transform.forward.normalized.z); }

        // Backward movement
        if (Input.GetKey(KeyCode.S)) 
        { rb.velocity += -speedScale * new Vector3(followCamera.transform.forward.normalized.x, 0.0f, followCamera.transform.forward.normalized.z); }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        { rb.velocity += speedScale * new Vector3(followCamera.transform.right.normalized.x, 0.0f, followCamera.transform.right.normalized.z); }

        // Left movement
        if (Input.GetKey(KeyCode.A))
        { rb.velocity += -speedScale * new Vector3(followCamera.transform.right.normalized.x, 0.0f, followCamera.transform.right.normalized.z); }

        // Sprint logic including changing player state to and from sprinting
        if (Input.GetKey(KeyCode.LeftShift) && currentPlayerState != PlayerState.Idle)
        {
            currentPlayerState = PlayerState.Sprinting;
            speedScale = SPRINT_SPEED_SCALE;
        }
        else
        {
            currentPlayerState = PlayerState.Walking;
            speedScale = WALK_SPEED_SCALE;
        }

        // If no movement, player state is set to idle
        if (rb.velocity.magnitude <= 0.001f) { currentPlayerState = PlayerState.Idle; }

        // If movement in the x or z direction and the player is not sprinting, player state is set to walking
        if ((rb.velocity.x > 0.01f || rb.velocity.z > 0.01f)
            && currentPlayerState != PlayerState.Sprinting)
        { currentPlayerState = PlayerState.Walking; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
