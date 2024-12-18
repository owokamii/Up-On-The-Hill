using UnityEngine;

public class Mailbox : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Dad dad;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer mailboxSP;
    [SerializeField] private Sprite[] mailboxS;

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
    public bool interactedMailbox_2;
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
        FindObjectOfType<AudioManager>().PlaySFX("MailboxOpen");

        interactionCD = true;

        interacted = true;
        DisableSpeechBubble();
        cinematicAnimator.SetBool("Cinematic", true);
        dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        FindObjectOfType<AudioManager>().PlaySFX("MailboxClose");

        Invoke("InteractionCD", 2.0f);

        interacted = false;
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void ObtainedItem()
    {
        if(interactedMailbox_2)
        {
            FindObjectOfType<AudioManager>().PlaySFX("ObtainedMail");
            mailPanel.SetActive(true);
            isActive = true;
        }
    }

    private void UpdateDialogueBefore()
    {
        if (obtainedMail)
        {
            mailboxSP.sprite = mailboxS[1];
        }
        else
        {
            mailboxSP.sprite = mailboxS[0];
        }

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
        if (obtainedMail)
        {
            mailboxSP.sprite = mailboxS[3];
        }
        else
        {
            mailboxSP.sprite = mailboxS[2];
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
