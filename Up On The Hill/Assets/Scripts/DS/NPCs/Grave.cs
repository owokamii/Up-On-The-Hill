using UnityEngine;

public class Grave : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Dad2 dad2;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private CapsuleCollider2D graveTrigger;
    [SerializeField] private SpriteRenderer boqSP;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] dialogueBox;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos = 0;
    private bool inRange;
    private bool interacted;
    private bool interactionCD;

    // triggers
    private bool interactedDad2;
    private bool interactedGrave;

    // get set
    public bool GetInteractedGrave { get => interactedGrave; }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
                UpdateDialogueAfter();
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
        dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void UpdateDialogueBefore()
    {
        if (!interactedDad2)
        {
            interactedDad2 = dad2.GetInteractedDad2;

            if (interactedDad2)
            {

                pos++;
            }
        }
    }

    private void UpdateDialogueAfter()
    {
        if (interactedDad2)
        {
            graveTrigger.enabled = false;
            interactedGrave = true;
            boqSP.enabled = true;
        }
    }

    private void InteractionCD()
    {
        interactionCD = false;
    }

    private void EnableSpeechBubble()
    {
        playerController.enabled = true;
        if(graveTrigger.isActiveAndEnabled)
        {
            speechBubble.enabled = true;
        }
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
