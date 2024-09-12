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

    private bool bgmStarted;

    private void CinematicZoom()
    {
        playerCamera.Follow = indoorCamera;
        StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomOut));
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