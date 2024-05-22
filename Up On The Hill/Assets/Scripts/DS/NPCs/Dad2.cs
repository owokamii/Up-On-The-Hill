using Cinemachine;
using UnityEngine;

public class Dad2 : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Grave grave;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator dadAnimator;

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
    public bool GetInteractedDad2 { get => interactedDad2; }

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
        dadAnimator.SetBool("Interacted", true);
        dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        dadAnimator.SetBool("Interacted", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void UpdateDialogueBefore()
    {
        if (!interactedDad2)
        {
            interactedDad2 = true;
        }

        if(!interactedGrave)
        {
            interactedGrave = grave.GetInteractedGrave;

            if(interactedGrave)
            {
                pos++;
            }
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
