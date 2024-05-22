using UnityEngine;

public class Flower : MonoBehaviour
{
    [Header("Game Scripts")]
    [SerializeField] private PlayerController playerController;

    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private CapsuleCollider2D flowerTrigger;
    [SerializeField] private SpriteRenderer flowerSP;

    [Header("Game Objects")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject flowerPanel;

    [Header("Parameters")]
    [SerializeField] private float invokeSpeechBubble = 1.5f;

    private int pos;
    private bool inRange;
    private bool isActive;
    private bool interacted;
    private bool interactionCD;
    private bool sfxPlayed;

    public bool GetIsActive { get => isActive; }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox.activeSelf && interacted)
            {
                if(!isActive)
                {
                    Invoke("ObtainedItem", invokeSpeechBubble);
                }

                if (!flowerPanel.activeSelf && isActive)
                {
                    EndDialogue();
                    DisableFlower();
                }
            }
            else if (!interacted && !interactionCD && Input.GetButtonDown("Interact"))
            {
                StartDialogue();
            }
        }
    }

    private void DisableFlower()
    {
        flowerTrigger.enabled = false;
        flowerSP.sprite = null;
    }

    private void StartDialogue()
    {
        playerController.GetFlowerNum += 1;
        pos = playerController.GetFlowerNum;

        interactionCD = true;

        interacted = true;
        DisableSpeechBubble();
        cinematicAnimator.SetBool("Cinematic", true);
        dialogueBox.SetActive(true);
    }

    private void EndDialogue()
    {
        Invoke("InteractionCD", 2.0f);

        interacted = false;
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnableSpeechBubble", invokeSpeechBubble);
    }

    private void ObtainedItem()
    {
        if (!sfxPlayed)
        {
            switch (pos)
            {
                case 1:
                    FindObjectOfType<AudioManager>().PlaySFX("1");
                    Debug.Log("test");
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().PlaySFX("2");
                    Debug.Log("test");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().PlaySFX("3");
                    Debug.Log("test");
                    break;
                case 4:
                    FindObjectOfType<AudioManager>().PlaySFX("4");
                    Debug.Log("test");
                    break;
                case 5:
                    FindObjectOfType<AudioManager>().PlaySFX("5");
                    Debug.Log("test");
                    break;
                default:
                    break;
            }

            sfxPlayed = true;
        }

        flowerPanel.SetActive(true);
        isActive = true;
    }

    private void InteractionCD()
    {
        interactionCD = false;
    }

    private void EnableSpeechBubble()
    {
        playerController.enabled = true;
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
