using UnityEngine;

public class G : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private D dad;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private CapsuleCollider2D gateTrigger;
    [SerializeField] private BoxCollider2D gateCollider;
    [SerializeField] private SpriteRenderer gateSP;
    [SerializeField] private Sprite gateS;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos;
    private bool inRange;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactedGate_1;
    private bool obtainedKey;

    // get set
    public bool GetInteractedGate_1 { get => interactedGate_1; }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
                UpdateDialogueAfter();
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
        FindObjectOfType<AudioManager>().PlaySFX("GateLocked");

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

    private void UpdateDialogueBefore()
    {
        // event 5 - interact with gate with key
        if (!obtainedKey && interactedGate_1)
        {
            obtainedKey = dad.GetObtainedKey;

            if (obtainedKey)
            {
                pos++;
            }
        }

        // event 1 - interact with gate locked
        if (!interactedGate_1)
        {
            interactedGate_1 = true;
        }
    }

    private void UpdateDialogueAfter()
    {
        // event 5.1 - unlock gate
        if (obtainedKey)
        {
            FindObjectOfType<AudioManager>().PlaySFX("GateUnlocked");
            obtainedKey = false;
            gateTrigger.enabled = false;
            gateCollider.enabled = false;
            gateSP.sprite = gateS;
        }
    }

    private void InteractionCD()
    {
        interactionCD = false;
    }

    private void EnableSpeechBubble()
    {
        playerController.enabled = true;
        //speechBubble.enabled = true;
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
