using UnityEngine;

public class FlowerInteract : MonoBehaviour
{
    [SerializeField] private Animator flower1Animator;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject flower1Panel;

    private bool interacted = false;
    private bool inRange = false;

    private void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueBox.activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.E))
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
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        Debug.Log("end");
        Invoke("SpeechBubbleEnable", 1.0f);
        cinematicAnimator.SetBool("Cinematic", false);      // black bars disable
        Invoke("ObtainFlower", 1.0f);
    }

    private void ObtainFlower()
    {
        flower1Panel.gameObject.SetActive(true);
        Invoke("UnobtainedFlower", 2.0f);
    }

    private void UnobtainedFlower()
    {
        flower1Animator.SetBool("ObtainedFlower1", true);
        DestroyFlower();

    }

    private void DestroyFlower()
    {
        Destroy(gameObject);
    }
}
