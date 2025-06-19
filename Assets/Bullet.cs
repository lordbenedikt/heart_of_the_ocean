using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
