using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private F_Movement fMovement;
    [SerializeField] private Racoon racoon;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator frogAnimator;
    [SerializeField] private CapsuleCollider2D frogTrigger;
    [SerializeField] private SpriteRenderer frogSP;
    [SerializeField] private AudioSource frogAudioSource;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject frogPanel;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos;
    private bool inRange;
    private bool isActive;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactRacoon_1;
    private bool obtainedFrog;

    public bool GetObtainedFrog { get => obtainedFrog; }

    private void Awake()
    {
        frogAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                if (!isActive)
                {
                    ObtainedItem();
                }

                if(!frogPanel.activeSelf)
                {
                    EndDialogue();
                    if (isActive)
                    {
                        DisableFrog();
                    }
                }
            }
            else if (!interacted && !interactionCD && Input.GetButtonDown("Interact"))
            {
                UpdateDialogueBefore();
                StartDialogue();
            }
        }
    }

    private void DisableFrog()
    {
        frogAnimator.enabled = false;
        frogTrigger.enabled = false;
        frogSP.sprite = null;
    }

    private void StartDialogue()
    {
        interactionCD = true;

        interacted = true;
        DisableSpeechBubble();
        cinematicAnimator.SetBool("Cinematic", true);
        dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void ObtainedItem()
    {
        if (interactRacoon_1)
        {
            frogPanel.SetActive(true);
            isActive = true;
            interactRacoon_1 = false;
            frogAudioSource.Stop();
            FindObjectOfType<AudioManager>().PlaySFX("ObtainedFrog");
        }
    }

    private void UpdateDialogueBefore()
    {
        // event 6 - interact with racoon
        if (!interactRacoon_1)
        {
            interactRacoon_1 = racoon.GetInteractRacoon_1;

            if(interactRacoon_1)
            {
                pos++;
                obtainedFrog = true;
            }
        }
    }

    private void InteractionCD()
    {
        interactionCD = false;
    }

    private void EnableSpeechBubble()
    {
        fMovement.enabled = true;
        playerController.enabled = true;
        if(!isActive)
        {
            speechBubble.enabled = true;
        }
    }

    private void DisableSpeechBubble()
    {
        fMovement.enabled = false;
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
}
