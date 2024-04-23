using UnityEngine;

public class DadInteract : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator keyAnimator;
    [SerializeField] private Animator dadAnimator;
    [SerializeField] private Animator cinematicAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject keyPanel;

    [Header("Scripts")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private GateInteract gateInteract;
    [SerializeField] private MailboxInteract mailboxInteract;

    private bool interacted = false;
    private bool inRange = false;
    private int pos = 0;

    private bool mailboxDialogue1 = false;
    private bool gateDialogue1 = false;

    public bool GetMailboxDialogue1 { get { return mailboxDialogue1; } }
    public bool GetGateDialogue1 { get { return gateDialogue1; } }

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
                UpdateDialogue();
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

        if(pos == 2)
        {
            gateDialogue1 = true;
            Invoke("ObtainedKey", 1.0f);
        }
    }

    private void UpdateDialogue()
    {
        if (gateInteract.GetDadDialogue1 && pos == 0)           // first interaction with dad
        {
            mailboxDialogue1 = true;
            pos = 1;
        }
        else if (mailboxInteract.GetDadDialogue3 && pos == 1)   // dad tell you to get mail
        {
            pos = 2;
        }
        else if(pos == 2)                                       // dad gave u the keys
        {
            pos = 3;
        }
    }

    private void ObtainedKey()
    {
        keyPanel.gameObject.SetActive(true);
        Invoke("UnobtainedMail", 2.0f);
    }

    private void UnobtainedKey()
    {
        keyAnimator.SetBool("ObtainedLetter", true);
    }
}
