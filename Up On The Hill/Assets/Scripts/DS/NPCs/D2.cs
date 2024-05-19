using Cinemachine;
using System.Collections;
using UnityEngine;

public class D2 : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GraveInteract grave;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator dadAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform indoorCamera;

    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;

    private int pos = 0;
    private bool inRange;
    private bool bgmStarted;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactedDad2;
    private bool interactedGrave;

    // get set
    public bool GetInteractedDad2 { get => interactedDad2; }

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
            else if (!interacted && !interactionCD && Input.GetButtonDown("Interact"))
            {
                UpdateDialogueBefore();
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        if(!bgmStarted)
        {
            FindObjectOfType<AudioManager>().PlaySFX("Piano");
            bgmStarted = true;
        }

        CinematicZoom();

        interactionCD = true;

        interacted = true;
        DisableSpeechBubble();
        cinematicAnimator.SetBool("Cinematic", true);
        dadAnimator.SetBool("Interacted", true);
        dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        dadAnimator.SetBool("Interacted", false);
        //cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void UpdateDialogueBefore()
    {
        if (!interactedDad2)
        {
            interactedDad2 = true;
        }

        if(!interactedGrave)
        {
            interactedGrave = grave.GetInteractedGrave;

            if(interactedGrave)
            {
                pos++;
            }
        }
    }

    private void InteractionCD()
    {
        interactionCD = false;
    }

    private void EnableSpeechBubble()
    {
        playerController.enabled = true;
        speechBubble.enabled = true;
    }

    private void DisableSpeechBubble()
    {
        playerController.enabled = false;
        speechBubble.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        speechBubble.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        speechBubble.enabled = false;
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
