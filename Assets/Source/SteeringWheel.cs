using UnityEngine;

public class SteeringControl : MonoBehaviour, IMountable
{
    public float acceleration = 6f;
    public float maxSpeed = 5f;
    private Rigidbody shipRigidBody;

    public GameObject ship;

    void Awake()
    {
        if (shipRigidBody == null && transform.parent != null)
        {
            shipRigidBody = transform.parent.GetComponent<Rigidbody>();
        }
        if (shipRigidBody == null)
        {
            Debug.LogError("No Rigidbody found on this GameObject or its parent.");
        }
        // Lock rotation and Z position for 2D-like movement
        shipRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void Steer(float horizontal, float vertical)
    {
        // Only accelerate in X and Y, keep Z unchanged
        Vector3 inputDirection = new Vector3(horizontal, vertical, 0f).normalized;
        shipRigidBody.AddForce(inputDirection * acceleration * shipRigidBody.mass);

        // Apply damping to the ship's velocity
        shipRigidBody.linearVelocity *= 0.98f;

        // Clamp velocity to maxSpeed in X and Y
        Vector3 clampedVelocity = shipRigidBody.linearVelocity;
        clampedVelocity.z = 0f;
        if (clampedVelocity.magnitude > maxSpeed)
        {
            clampedVelocity = clampedVelocity.normalized * maxSpeed;
        }
        shipRigidBody.linearVelocity = clampedVelocity;
    }

    public void Activate() {}
}