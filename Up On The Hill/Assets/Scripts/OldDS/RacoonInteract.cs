using UnityEngine;

public class RacoonInteract : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator racoonAnimator;
    [SerializeField] private Animator boqAnimator;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite frogSprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject racoon;
    [SerializeField] private GameObject frog;
    [SerializeField] private GameObject racoonEvent;
    [SerializeField] private GameObject boqPanel;


    [Header("Scripts")]
    //[SerializeField] private DialogueSystem dialogueSystem;

    private bool interacted = false;
    private bool inRange = false;
    private int pos = 0;

    private void Update()
    {
        if(!racoon)
        {
            racoonAnimator.SetBool("RacoonIdle", true);
        }

        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.E))
            {
                if (racoonEvent)
                {
                    Destroy(racoonEvent);
                }

                UpdateDialogue();
                StartDialogue();
            }
        }
    }

    private void ActivateDialogue()
    {
        dialogueBox[pos].SetActive(true);
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
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        interacted = false;
        Debug.Log("end");
        cinematicAnimator.SetBool("Cinematic", false);      // black bars

       if(pos == 1)
        {
            sp.sprite = frogSprite;
            Invoke("ObtainedBoq", 1.0f);
        }
    }

    private void UpdateDialogue()
    {
        if (!frog && pos == 0)           // first interaction with dad
        {
            pos = 1;
        }
    }

    private void ObtainedBoq()
    {
        boqPanel.gameObject.SetActive(true);
        Invoke("UnobtainedBoq", 2.0f);
    }

    private void UnobtainedBoq()
    {
        boqAnimator.SetBool("ObtainedBoq", true);
    }
}
