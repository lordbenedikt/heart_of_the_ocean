using UnityEngine;

public class SteeringControl : MonoBehaviour, IMountable
{
    public ShipController ship;

    public void Steer(float horizontal, float vertical)
    {
        // Only accelerate in X and Y, keep Z unchanged
        Vector3 inputDirection = new Vector3(horizontal, vertical, 0f).normalized;
        ship.Accelerate(inputDirection);
    }

    public void Activate() { }
}