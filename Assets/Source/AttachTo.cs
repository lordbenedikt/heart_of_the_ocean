using UnityEngine;

public class AttachTo : MonoBehaviour
{
    public Transform target; // The target to attach to
                             // Update is called once per frame
    
    void Update()
    {
        if (target != null)
        {
            // Set the position and rotation of this object to match the target
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        else
        {
            Debug.LogWarning("Target is not set for AttachTo script on " + gameObject.name);
        }    
    }
}
