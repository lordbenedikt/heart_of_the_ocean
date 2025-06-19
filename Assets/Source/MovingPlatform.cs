using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    private int currentTarget = 0;

    void Update()
    {
        if (points.Length == 0) return;

        Transform target = points[currentTarget];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentTarget = (currentTarget + 1) % points.Length;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var point in points)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
}
