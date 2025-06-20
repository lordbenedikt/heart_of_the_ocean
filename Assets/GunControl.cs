using System;
using UnityEngine;

public class GunControl : MonoBehaviour, IMountable
{
    public Transform hinge;
    public Transform gunTip;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public bool invertAxes = false;
    public float steeringSpeed = 3f;

    public float minAngle = -70f;
    public float maxAngle = 70f;

    public void Activate()
    {
        GameObject obj = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        Rigidbody bulletRb = obj.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = gunTip.TransformDirection(Vector3.forward) * bulletSpeed;
    }

    public void Steer(float horizontal, float vertical)
    {
        // Get current local rotation
        Vector3 localEuler = hinge.localEulerAngles;

        // Convert to -180..180 range for proper clamping
        float currentZ = localEuler.z;
        if (currentZ > 180f) currentZ -= 360f;

        // Calculate new rotation
        float zDelta = (invertAxes ? 1 : -1) * vertical * steeringSpeed;
        float targetZ = Mathf.Clamp(currentZ + zDelta, minAngle, maxAngle);

        // Apply rotation, keeping other axes unchanged
        hinge.localRotation = Quaternion.Euler(localEuler.x, localEuler.y, targetZ);
    }
}
