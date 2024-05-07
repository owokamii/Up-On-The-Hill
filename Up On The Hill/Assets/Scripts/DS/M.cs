using UnityEngine;

public class M : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private D dad;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer mailboxSP;
    [SerializeField] private Sprite mailboxS;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject mailPanel;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos = 0;
    private bool inRange;
    private bool isActive;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactedDad_2;
    private bool interactedMailbox_2;
    private bool obtainedMail;

    // get set
    public bool GetObtainedMail { get => obtainedMail; }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                if(!isActive)
                {
                    ObtainedItem();
                }

                if (!mailPanel.activeSelf)
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
        if(interactedMailbox_2)
        {
            mailPanel.SetActive(true);
            isActive = true;
        }
    }

    private void UpdateDialogueBefore()
    {
        // event 3.1 - obtained mail
        if (interactedMailbox_2)
        {
            interactedMailbox_2 = false;
            pos++;
        }

        // event 3 - interact with mailbox
        if (!interactedDad_2)
        {
            interactedDad_2 = dad.GetInteractedDad_2;

            if (interactedDad_2)
            {
                // change sprite to mailbox with mail
                interactedMailbox_2 = true;
                obtainedMail = true;
                pos++;
            }
        }
    }

    private void UpdateDialogueAfter()
    {
        if (interactedMailbox_2)
        {
            mailboxSP.sprite = mailboxS;
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
