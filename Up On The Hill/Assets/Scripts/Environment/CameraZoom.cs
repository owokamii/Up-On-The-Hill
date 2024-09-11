using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float cameraZoomIn;
    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;

    // Define fixed positions for zooming in and out
    private float zoomInY = 0f;  // Y level when zoomed in
    private float zoomOutY = 8f;  // Y level when zoomed out

    private Coroutine zoomAndMoveCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (zoomAndMoveCoroutine != null)
                StopCoroutine(zoomAndMoveCoroutine);

            // Zoom out and move camera to y = zoomOutY (20)
            zoomAndMoveCoroutine = StartCoroutine(ZoomAndMoveCoroutine(cameraZoomOut, zoomOutY));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (zoomAndMoveCoroutine != null)
                StopCoroutine(zoomAndMoveCoroutine);

            // Zoom in and move camera to y = zoomInY (0)
            zoomAndMoveCoroutine = StartCoroutine(ZoomAndMoveCoroutine(cameraZoomIn, zoomInY));
        }
    }

    private IEnumerator ZoomAndMoveCoroutine(float targetZoom, float targetY)
    {
        float elapsedTime = 0f;
        float startZoom = playerCamera.m_Lens.OrthographicSize;
        float startY = cameraTransform.position.y;  // Get the current Y position at the start of the zoom

        // Debug the target Y to ensure it's correct
        Debug.Log($"Starting Zoom: startY = {startY}, targetY = {targetY}");

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Ensure t stays between 0 and 1
            t = Mathf.Clamp01(t);

            // Apply ease-out function for smoother transitions
            t = t * (2 - t); // Quadratic ease-out function

            // Lerp both the zoom and camera position simultaneously using eased t
            playerCamera.m_Lens.OrthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            cameraTransform.position = new Vector3(cameraTransform.position.x, Mathf.Lerp(startY, targetY, t), cameraTransform.position.z);

            // Debug to check the current camera position
            Debug.Log($"Lerping: Camera Y = {cameraTransform.position.y}, t = {t}");

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final values to ensure no inconsistencies
        playerCamera.m_Lens.OrthographicSize = targetZoom;
        cameraTransform.position = new Vector3(cameraTransform.position.x, targetY, cameraTransform.position.z);

        // Debug final position to ensure it's set correctly
        Debug.Log($"Final Position: Camera Y = {cameraTransform.position.y}");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(cameraTransform.position.y);
        }
    }
}