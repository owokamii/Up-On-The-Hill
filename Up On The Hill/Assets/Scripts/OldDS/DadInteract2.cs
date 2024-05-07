using Cinemachine;
using System.Collections;
using UnityEngine;

public class DadInteract2 : MonoBehaviour
{
    [SerializeField] private Animator dadAnimator;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform indoorCamera;

    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;

    private bool interacted = false;
    private bool inRange = false;
    private int pos = 0;

    private void Awake()
    {
        dadAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.E))
            {
                //UpdateDialogue();
                StartDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        speechBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        speechBubble.SetActive(false);
    }

    private void ActivateDialogue()
    {
        dialogueBox[pos].SetActive(true);
    }

    private void SpeechBubbleDisable()
    {
        speechBubble.SetActive(false);
    }

    private void SpeechBubbleEnable()
    {
        speechBubble.SetActive(true);
    }

    private void StartDialogue()
    {
        interacted = true;
        Debug.Log("start");
        cinematicAnimator.SetBool("Cinematic", true);   // black bars enable
        dadAnimator.SetBool("Interacted", true);        // dad changes animation
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        interacted = false;
        Debug.Log("end");
        dadAnimator.SetBool("Interacted", false);           // dad changes animation
        cinematicAnimator.SetBool("Cinematic", false);      // black bars
        // trigger cinematic zoom
        Invoke("CinematicZoom", 1.0f);
    }

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
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            float newSize = Mathf.Lerp(startSize, targetSize, t);
            playerCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.m_Lens.OrthographicSize = targetSize;
    }
}
