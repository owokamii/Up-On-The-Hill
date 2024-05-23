using UnityEngine;

public class R_Event : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Flower[] flower;

    [Header("Game Components")]
    [SerializeField] private Animator cinematicAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] flowers;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject racoonEvent;
    [SerializeField] private GameObject blockEvent1;
    [SerializeField] private GameObject racoon;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private bool initiated;
    private bool interacted;
    private bool eventEnded;
    private bool[] isActivated;

    private void Awake()
    {
        if (isActivated == null || isActivated.Length != flowers.Length)
        {
            isActivated = new bool[flowers.Length];
        }
    }

    private void Update()
    {
        if (!dialogueBox.activeSelf && interacted)
        {
            EndDialogue();
        }
        else if (!interacted && initiated)
        {
            StartDialogue();
            racoonEvent.SetActive(true);
            racoon.SetActive(true);
        }
    }

    private void StartDialogue()
    {
        FindObjectOfType<AudioManager>().PlaySFX("RacoonRun");

        initiated = false;
        interacted = true;
        DisableSpeechBubble();
        cinematicAnimator.SetBool("Cinematic", true);
        dialogueBox.SetActive(true);
    }

    private void EndDialogue()
    {
        interacted = false;
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void EnableSpeechBubble()
    {
        Debug.Log("enable");
        playerController.enabled = true;
    }

    private void DisableSpeechBubble()
    {
        Debug.Log("disable");
        playerController.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!eventEnded)
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                isActivated[i] = flowers[i].GetComponent<Flower>().GetIsActive;
            }
        }

        if (isActivated[0] && isActivated[1] && isActivated[2] && isActivated[3] && isActivated[4])
        {
            initiated = true;
            eventEnded = true;
            blockEvent1.SetActive(false);
            racoon.SetActive(true);

            for (int i = 0; i < flowers.Length; i++)
            {
                isActivated[i] = false;
            }
        }
    }
}
