using UnityEngine;

public class DadInteract : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator dadAnimator;
    [SerializeField] private Animator cinematicAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject dialogueBox;

    [Header("Scripts")]
    [SerializeField] private DialogueSystem dialogueSystem;

    private bool interacted = false;
    private bool inRange = false;

    private void Awake()
    {
        dadAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox.activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.Space))
            {
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
        dialogueBox.SetActive(true);
        //dialogueSystem.enabled = true;
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
        Debug.Log("test 1");
        cinematicAnimator.SetBool("Cinematic", true);   // black bars enable
        dadAnimator.SetBool("Interacted", true);        // dad changes animation
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        interacted = false;
        Debug.Log("test 2");
        dadAnimator.SetBool("Interacted", false);           // dad changes animation
        cinematicAnimator.SetBool("Cinematic", false);      // black bars disable                                                    
    }
}