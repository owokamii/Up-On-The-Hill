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

    private Coroutine zoomCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (zoomCoroutine != null)
                StopCoroutine(zoomCoroutine);

            playerCamera.Follow = indoorCamera;
            zoomCoroutine = StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomOut));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (zoomCoroutine != null)
                StopCoroutine(zoomCoroutine);

            playerCamera.Follow = player;
            zoomCoroutine = StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomIn));
        }
    }

    private IEnumerator ZoomCoroutine(float startSize, float targetSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            float newSize = Mathf.Lerp(startSize, targetSize, t);
            playerCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.m_Lens.OrthographicSize = targetSize;
    }
}