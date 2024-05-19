using UnityEngine;

public class GraveInteract : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private D2 dad2;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private SpriteRenderer sp;

    private int pos = 0;
    private bool inRange;
    private bool bgmStarted;
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
            /*if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
            }
            else if (!interacted && !interactionCD && Input.GetButtonDown("Interact"))
            {
                //UpdateDialogueBefore();
                StartDialogue();
            }*/
        }
    }

    /*private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if(!interactedDad2)
            {
                interactedDad2 = dad2.GetInteractedDad2;

                if(interactedDad2)
                {
                    sp.enabled = true;
                    interactedGrave = true;
                }
            }
        }
    }*/

    private void StartDialogue()
    {
        interactionCD = true;

        interacted = true;
        DisableSpeechBubble();
        //cinematicAnimator.SetBool("Cinematic", true);
        //dadAnimator.SetBool("Interacted", true);
        //dialogueBox[pos].SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        //dadAnimator.SetBool("Interacted", false);
        //cinematicAnimator.SetBool("Cinematic", false);
        //Invoke("EnableSpeechBubble", invokeSpeechBubble);
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
