using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform player;
    [SerializeField] private Transform indoorCamera;

    [SerializeField] private float cameraZoomIn;
    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCamera.Follow = indoorCamera;
        StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomOut));
        //playerCamera.m_Lens.OrthographicSize = cameraZoomOut;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCamera.Follow = player;
        StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomIn));
        //playerCamera.m_Lens.OrthographicSize = cameraZoomIn;
    }

    private IEnumerator ZoomCoroutine(float startSize, float targetSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            float newSize = Mathf.Lerp(startSize, targetSize, t);
            playerCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.m_Lens.OrthographicSize = targetSize;
    }
}