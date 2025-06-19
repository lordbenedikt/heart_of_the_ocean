using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class player_controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Transform currentPlatform;
    private Vector3 platformLastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Moving platform support
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - platformLastPosition;
            controller.Move(platformMovement);
            platformLastPosition = currentPlatform.position;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Detect if standing on a moving platform
        if (hit.moveDirection.y < -0.9f && hit.collider.tag == "MovingPlatform")
        {
            if (currentPlatform != hit.collider.transform)
            {
                currentPlatform = hit.collider.transform;
                platformLastPosition = currentPlatform.position;
            }
        }
        else if (hit.collider.tag != "MovingPlatform")
        {
            currentPlatform = null;
        }
    }
}