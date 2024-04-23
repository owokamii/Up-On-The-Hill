using UnityEngine;

public class GateInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject dialogueBox;

    private bool interacted = false;
    private bool inRange = false;
    private bool dadDialogue1 = false;

    public bool GetDadDialogue1 { get { return dadDialogue1; } }

    private void Update()
    {
        if(inRange)
        {
            if (!dialogueBox.activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.Space))
            {
                StartDialogue();
            }

            /*if(Input.GetKeyDown(KeyCode.Space))
            {
                boxCollider.enabled = false;
                spriteRenderer.sprite = sprite;
            }*/
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
    }
}
