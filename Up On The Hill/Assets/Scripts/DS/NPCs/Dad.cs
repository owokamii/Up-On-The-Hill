using UnityEngine;

public class Dad : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Gate gate;
    [SerializeField] private Mailbox mailbox;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator dadAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject keyPanel;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos = 0;
    private bool inRange;
    private bool isActive;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactedGate_1;
    private bool interactedDad_2;
    private bool obtainedMail;
    private bool obtainedKey;
    private bool dadConversation4;

    // get set
    public bool GetInteractedDad_2 { get => interactedDad_2; }
    public bool GetObtainedKey { get => obtainedKey; }

    private void Update()
    {
        if(inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                if (!isActive)
                {
                    ObtainedItem();
                }

                if (!keyPanel.activeSelf)
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
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void ObtainedItem()
    {
        if(obtainedMail)
        {
            FindObjectOfType<AudioManager>().PlaySFX("ObtainedKey");
            keyPanel.SetActive(true);
            isActive = true;
        }
    }

    private void UpdateDialogueBefore()
    {
        // event 4 - obtained key
        if (!obtainedMail)
        {
            obtainedMail = mailbox.GetObtainedMail;

            if (obtainedMail)
            {
                obtainedKey = true;
                dadConversation4 = true;
                pos++;
            }
        }

        // event 2 - interact with dad
        if (!interactedGate_1)
        {
            interactedGate_1 = gate.GetInteractedGate_1;

            if (interactedGate_1)
            {
                interactedDad_2 = true;
                pos++;
            }
        }
    }

    private void UpdateDialogueAfter()
    {
        // event 4.1 - after obtained key
        if (dadConversation4)
        {
            dadConversation4 = false;
            pos++;
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
