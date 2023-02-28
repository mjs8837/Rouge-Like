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
    private const float WALK_SPEED_SCALE = 3.5f;

    /// <summary>
    /// Constant scale at which sprint speed is multiplied by
    /// </summary>
    private const float SPRINT_SPEED_SCALE = 6.0f;

    /// <summary>
    /// Constant scale at which the player movement speed is multipled by
    /// </summary>
    private float speedScale = 2.5f;

    /// <summary>
    /// Current player movement state
    /// </summary>
    PlayerState currentPlayerState = PlayerState.Idle;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting delta x and y for mouse movement to control the camera
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float jump = Input.GetAxis("Jump");

        // Setting the camera to follow the player
        followCamera.transform.position = transform.position;

        // Setting up camera rotation for the player
        followCamera.transform.eulerAngles += CAMERA_SPEED_SCALE * new Vector3(-mouseY, mouseX, 0.0f);
        transform.eulerAngles = followCamera.transform.eulerAngles;

        rb.velocity = new Vector3(rb.velocity.x, jump * 5.0f, rb.velocity.z);

        // Forward movement
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = speedScale * new Vector3(followCamera.transform.forward.normalized.x, 0.0f, followCamera.transform.forward.normalized.z);
        }

        // Backward movement
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -speedScale * new Vector3(followCamera.transform.forward.normalized.x, 0.0f, followCamera.transform.forward.normalized.z);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = speedScale * new Vector3(followCamera.transform.right.normalized.x, 0.0f, followCamera.transform.right.normalized.z);
        }

        // Left movement
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -speedScale * new Vector3(followCamera.transform.right.normalized.x, 0.0f, followCamera.transform.right.normalized.z);
        }

        // Sprint logic
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedScale = SPRINT_SPEED_SCALE;
        }
        else
        {
            speedScale = WALK_SPEED_SCALE;
        }

        /*switch(GameManager.currentGameState)
        {

        }

        // Handling what happens during each player state to avoid checking things that don't need to be checked
        switch (currentPlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking: 
                break;
            case PlayerState.Sprinting:
                break;
            case PlayerState.Jumping:
                break;
            default:
                break;
        }*/
    }
}
