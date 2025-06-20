using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    private float lifetime = 5f;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DestroySelfAfterLifetime();
    }

    private void DestroySelfAfterLifetime()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Bullet collided with {collision.gameObject.name}");
        Destroy(gameObject);
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(10); // Assuming bullets deal 10 damage
        }
    }
}
