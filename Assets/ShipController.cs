using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody rb;
    public float shipAcceleration = 0.001f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Accelerate(Vector3 direction)
    {
        var normalizedDirection = direction.normalized;
        normalizedDirection.z = 0f;
        rb.linearVelocity += normalizedDirection * shipAcceleration;
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0f); // Ensure Z is always 0
    }
}