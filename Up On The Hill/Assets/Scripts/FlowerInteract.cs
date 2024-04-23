using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FlowerInteract : MonoBehaviour
{
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject dialogueBox;

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
        Invoke("DestroyFlower", 1.0f);
    }

    private void DestroyFlower()
    {
        Destroy(gameObject);
    }
}
