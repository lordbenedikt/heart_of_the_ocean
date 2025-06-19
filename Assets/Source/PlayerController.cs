using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController2_5D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 10f;
    public float gravity = -10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    [Header("Ceiling Check")]
    public Transform ceilingCheck;
    public float ceilingDistance = 0.3f;
    public LayerMask ceilingMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCeilinged;

    // Platform movement
    private Transform currentPlatform;
    private Vector3 platformLastPosition;
    private Vector3 platformDelta;

    private IMountable? closestMountable;
    private IMountable? mountedDevice;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mountable"))
        {
            closestMountable = other.gameObject.GetComponent<IMountable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mountable"))
        {
            closestMountable = null;
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log($"Is Grounded: {isGrounded}");

        // Ceiling check
        isCeilinged = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);
        Debug.Log($"Is Ceilinged: {isCeilinged}");

        // Setting velocity.y to a small negative value helps keep the player grounded.
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        // Stop upward velocity if hitting ceiling
        if (isCeilinged && velocity.y > 0)
            velocity.y = 0f;

        // Movement input
        float move = 0f;
        bool jumpPressed = false;

        if (mountedDevice != null)
        {
            float xAxisInput = Input.GetAxisRaw("Horizontal");
            float yAxisInput = Input.GetAxisRaw("Vertical");

            mountedDevice.Steer(xAxisInput, yAxisInput);

            if (Input.GetButtonDown("Jump"))
            {
                closestMountable.Activate();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                mountedDevice = null;
            }
        }
        else
        {
            move = Input.GetAxis("Horizontal");
            jumpPressed = Input.GetButtonDown("Jump");
            if (Input.GetKeyDown(KeyCode.E)) {
                if (closestMountable != null)
                {
                    // Attempt to interact with the closest steering wheel
                    mountedDevice = closestMountable;
                }
            }
        }

        Vector3 moveDir = new Vector3(move, 0, 0);
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Jump
        if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Platform movement
        if (currentPlatform != null)
        {
            platformDelta = currentPlatform.position - platformLastPosition;
            controller.Move(platformDelta); // ride the platform
            platformLastPosition = currentPlatform.position;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (((1 << hit.gameObject.layer) & groundMask) != 0)
        {
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                if (currentPlatform != hit.collider.transform)
                {
                    currentPlatform = hit.collider.transform;
                    platformLastPosition = currentPlatform.position;
                }
            }
            else
            {
                currentPlatform = null;
            }
        }
    }
}