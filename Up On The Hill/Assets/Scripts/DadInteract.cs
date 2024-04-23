using UnityEngine;

public class DadInteract : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator dadAnimator;
    [SerializeField] private Animator cinematicAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;

    [Header("Scripts")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private GateInteract gateInteract;

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

            else if (!interacted && Input.GetKeyDown(KeyCode.Space))
            {
                StartDialogue();
            }
        }

        UpdateDialogueBox();
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
        cinematicAnimator.SetBool("Cinematic", false);      // black bars disable                                                    
    }

    private void UpdateDialogueBox()
    {
        if(gateInteract.GetDadDialogue1)
        {
            pos = 1;
        }
    }
}
