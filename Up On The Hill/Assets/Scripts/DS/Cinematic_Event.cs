using System.Collections;
using Cinemachine;
using UnityEngine;

public class Cinematic_Event : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private Animator cinematicAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject blockEvent3;

    [Header("Parameters")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform indoorCamera;
    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;
    [SerializeField] private Vector3 cameraOffset;

    private bool bgmStarted;
    private Vector3 initialCameraPosition;

    private void CinematicZoom()
    {
        initialCameraPosition = indoorCamera.position;

        StartCoroutine(ZoomAndMoveCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomOut));
    }

    private IEnumerator ZoomAndMoveCoroutine(float startSize, float targetSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            float newSize = Mathf.Lerp(startSize, targetSize, t);
            playerCamera.m_Lens.OrthographicSize = newSize;

            indoorCamera.position = Vector3.Lerp(initialCameraPosition, initialCameraPosition + cameraOffset, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.m_Lens.OrthographicSize = targetSize;
        indoorCamera.position = initialCameraPosition + cameraOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bgmStarted)
        {
            CinematicZoom();
            cinematicAnimator.SetBool("Cinematic", true);
            FindObjectOfType<AudioManager>().PlaySFX("Piano");
            blockEvent3.SetActive(true);
            bgmStarted = true;
        }
    }
}
