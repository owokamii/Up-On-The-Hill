using UnityEngine;

public class Racoon : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Frog frog;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer flowerSP;
    [SerializeField] private Sprite frogS;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject bouquetPanel;
    [SerializeField] private GameObject blockEvent2;
    [SerializeField] private GameObject dad1;
    [SerializeField] private GameObject dad2;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos = 0;
    private bool inRange;
    private bool isActive;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactRacoon_1;
    private bool obtainedFrog;

    // get set
    public bool GetInteractRacoon_1 { get => interactRacoon_1; }

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

                if (!bouquetPanel.activeSelf)
                {
                    EndDialogue();
                    UpdateDialogueAfter();
                }
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
        FindObjectOfType<AudioManager>().PlaySFX("RacoonNoise");

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
        if (obtainedFrog)
        {
            FindObjectOfType<AudioManager>().PlaySFX("ObtainedBoq");
            bouquetPanel.SetActive(true);
            isActive = true;
        }
    }

    private void UpdateDialogueBefore()
    {
        // event 6 - interact with racoon
        if (!interactRacoon_1)
        {
            interactRacoon_1 = true;
        }

        if(!obtainedFrog)
        {
            obtainedFrog = frog.GetObtainedFrog;

            if(obtainedFrog)
            {
                blockEvent2.SetActive(false);
                dad1.SetActive(false);
                dad2.SetActive(true);
                pos++;
            }
        }
    }

    private void UpdateDialogueAfter()
    {
        if (obtainedFrog)
        {
            flowerSP.sprite = frogS;
            pos = 2;
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
}
