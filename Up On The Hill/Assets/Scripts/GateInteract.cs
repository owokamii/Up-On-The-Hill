using UnityEngine;

public class GateInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;

    [Header("Scripts")]
    [SerializeField] private DadInteract dadInteract;

    private bool interacted = false;
    private bool inRange = false;
    public int pos = 0;

    private bool dadDialogue1 = false;

    public bool GetDadDialogue1 { get { return dadDialogue1; } }

    private void Update()
    {
        if(inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.Space))
            {
                UpdateDialogue();
                StartDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        if(speechBubble)
        {
            speechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        if (speechBubble)
        {
            speechBubble.SetActive(false);
        }
    }

    private void ActivateDialogue()
    {
        dialogueBox[pos].SetActive(true);
    }

    private void SpeechBubbleDisable()
    {
        if (speechBubble)
        {
            speechBubble.SetActive(false);
        }
    }

    private void SpeechBubbleEnable()
    {
        if (speechBubble)
        {
            speechBubble.SetActive(true);
        }
    }

    private void StartDialogue()
    {
        interacted = true;
        Debug.Log("start");
        cinematicAnimator.SetBool("Cinematic", true);   // black bars enable
        Invoke("SpeechBubbleDisable", 1.0f);
        //dadAnimator.SetBool("Interacted", true);        // dad changes animation
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        interacted = false;
        Debug.Log("end");
        Invoke("SpeechBubbleEnable", 1.0f);
        //dadAnimator.SetBool("Interacted", false);           // dad changes animation
        cinematicAnimator.SetBool("Cinematic", false);      // black bars disable
        dadDialogue1 = true;

        if (pos == 1) // after gate is unlocked
        {
            spriteRenderer.sprite = sprite;
            boxCollider.enabled = false;
            capsuleCollider.enabled = false;
            Destroy(speechBubble);
        }
    }

    private void UpdateDialogue()
    {
        if (dadInteract.GetGateDialogue1 && pos == 0)       // gate is locked
        {
            pos = 1;
        }
    }
}
