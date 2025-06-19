using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float acceleration = 6f;
    public float maxSpeed = 5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Lock rotation and Z position for 2D-like movement
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Only accelerate in X and Y, keep Z unchanged
        Vector3 inputDirection = new Vector3(moveX, moveY, 0f).normalized;
        rb.AddForce(inputDirection * acceleration * rb.mass);

        // Clamp velocity to maxSpeed in X and Y
        Vector3 clampedVelocity = rb.linearVelocity;
        clampedVelocity.z = 0f;
        if (clampedVelocity.magnitude > maxSpeed)
        {
            clampedVelocity = clampedVelocity.normalized * maxSpeed;
        }
        rb.linearVelocity = clampedVelocity;
    }
}