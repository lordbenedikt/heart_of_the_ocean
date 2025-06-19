using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ship_controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Set velocity directly for 2D Rigidbody
        rb.linearVelocity = new Vector2(moveX, moveY) * moveSpeed;
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}
