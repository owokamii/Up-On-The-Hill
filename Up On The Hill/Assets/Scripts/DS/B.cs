using UnityEngine;

public class B : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;

    [Header("Game Components")]
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator playerAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject dialogueBox;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;
    [SerializeField] private float lerpDistance = 5.0f;
    [SerializeField] private float lerpDuration = 1.0f;

    private bool initiated;
    private bool interacted;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float lerpTimer;

    private void Update()
    {
        if (!dialogueBox.activeSelf && interacted)
        {
            LerpPlayerBack();
            EndDialogue();
        }
        else if (!interacted && initiated)
        {
            StartDialogue();
        }

        if (lerpTimer > 0)
        {
            float t = 1 - (lerpTimer / lerpDuration);
            playerController.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            lerpTimer -= Time.deltaTime;

            if (lerpTimer <= 0)
            {
                lerpTimer = 0;
            }
        }
    }

    private void StartDialogue()
    {
        initiated = false;
        interacted = true;
        DisableSpeechBubble();
        dialogueBox.SetActive(true);
    }

    private void EndDialogue()
    {
        interacted = false;
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void LerpPlayerBack()
    {
        initialPosition = playerController.transform.position;
        targetPosition = playerController.transform.position - playerController.transform.right * lerpDistance;
        lerpTimer = lerpDuration;
    }

    private void EnableSpeechBubble()
    {
        playerController.enabled = true;
    }

    private void DisableSpeechBubble()
    {
        playerController.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        initiated = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        initiated = false;
    }
}
