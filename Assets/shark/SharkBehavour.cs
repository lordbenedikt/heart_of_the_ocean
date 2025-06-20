using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SharkBehavour : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public float chaseSpeed = 6f;
    public float patrolRadius = 10f;
    public float detectionRadius = 15f;
    public float attackDistance = 3f;
    public float patrolTurnSpeed = 30f;

    public Transform player; // Exposed in inspector

    private Rigidbody rb;
    private Animator animator;
    private Vector3 patrolCenter;
    private float patrolAngle = 0f;
    private bool isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        patrolCenter = transform.position;
        // Start swimming animation
        if (animator != null)
            animator.SetBool("Swimming", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Chase player
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // Patrol in a circle
            Patrol();
        }

        // Always update animator parameters based on state
        if (animator != null)
        {
            animator.SetBool("Swimming", !isAttacking);
            animator.SetBool("Biting", isAttacking);
        }
    }

    void Patrol()
    {
        isAttacking = false;

        patrolAngle += patrolTurnSpeed * Time.deltaTime;
        float rad = patrolAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * patrolRadius;
        Vector3 targetPos = patrolCenter + offset;
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 nextPos = transform.position + dir * patrolSpeed * Time.deltaTime;
        rb.MovePosition(nextPos);
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
    }

    void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackDistance)
        {
            // Stay in place and keep attacking
            isAttacking = true;
            // Optionally: Add damage to player here
            return;
        }

        isAttacking = false;
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * chaseSpeed * Time.deltaTime;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }
}
