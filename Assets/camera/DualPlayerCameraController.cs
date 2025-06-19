using UnityEngine;

public class DualPlayerCameraController : MonoBehaviour
{
    [Header("Targets")]
    public Transform player1;
    public Transform player2;

    [Header("Movement")]
    [Tooltip("How quickly the camera follows the players.")]
    public float smoothSpeed = 0.125f;
    [Tooltip("The offset from the center point of the players.")]
    public Vector3 offset;

    [Header("Zoom")]
    [Tooltip("The minimum field of view (zoomed in).")]
    public float minFov = 20f;
    [Tooltip("The maximum field of view (zoomed out).")]
    public float maxFov = 60f;
    [Tooltip("The speed of the scroll wheel zoom.")]
    public float zoomSpeed = 25f;

    private Camera cam;
    private float initialZ;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("DualPlayerCameraController requires a Camera component on the same GameObject.");
            this.enabled = false;
            return;
        }
        // Store the initial Z position of the camera.
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            // Do nothing if the targets are not assigned.
            return;
        }

        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        // Find the center point between the two players.
        Vector3 centerPoint = (player1.position + player2.position) / 2f;

        // Determine the desired position for the camera.
        Vector3 desiredPosition = centerPoint + offset;

        // Use Lerp for a smooth transition.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Apply the new position but keep the original Z-axis value.
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, initialZ);
    }

    void HandleZoom()
    {
        // Get input from the mouse scroll wheel.
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new field of view.
        float newFov = cam.fieldOfView - scroll * zoomSpeed;

        // Clamp the FOV to the defined min and max values.
        cam.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
    }
}