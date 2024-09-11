using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public float minCameraDistance = 0f; // Minimum camera distance
    public float maxCameraDistance = 8f; // Maximum camera distance
    public float zoomInValue = 9f; // Zoom in distance
    public float zoomOutValue = 16f; // Zoom out distance

    public float easeDuration = 0.5f; // Duration for the easing effect
    private float targetCameraDistance; // The distance we are zooming towards
    private float currentVelocity = 0f; // Smoothing velocity for Mathf.SmoothDamp
    private Vector3 initialPosition; // Initial camera position to track

    private void Start()
    {
        // Initialize the target to the current camera's vertical position
        targetCameraDistance = cameraTransform.position.y;
        initialPosition = cameraTransform.position;
    }

    private void Update()
    {
        // Get input for zooming (you can adjust this for your needs)
        if (Input.GetKeyDown(KeyCode.Z)) ZoomIn();
        if (Input.GetKeyDown(KeyCode.X)) ZoomOut();

        // Smoothly move the camera towards the target distance
        float newDistance = Mathf.SmoothDamp(cameraTransform.position.y, targetCameraDistance, ref currentVelocity, easeDuration);

        // Clamp the zoom distance between min and max values
        newDistance = Mathf.Clamp(newDistance, minCameraDistance, maxCameraDistance);

        // Apply the new distance to the camera's position
        cameraTransform.position = new Vector3(cameraTransform.position.x, newDistance, cameraTransform.position.z);
    }

    public void ZoomIn()
    {
        // Set the target distance for zooming in
        targetCameraDistance = Mathf.Max(minCameraDistance, cameraTransform.position.y - zoomInValue);
    }

    public void ZoomOut()
    {
        // Set the target distance for zooming out
        targetCameraDistance = Mathf.Min(maxCameraDistance, cameraTransform.position.y + zoomOutValue);
    }
}