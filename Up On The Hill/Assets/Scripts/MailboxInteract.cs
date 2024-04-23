using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class MailboxInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator letterAnimator;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject[] dialogueBox;
    [SerializeField] private GameObject mailPanel;

    [Header("Scripts")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private DadInteract dadInteract;

    private bool interacted = false;
    private bool inRange = false;
    private int pos = 0;

    private bool dadDialogue3 = false;

    public bool GetDadDialogue3 { get { return dadDialogue3; } }

    private void Update()
    {
        if (inRange)
        {
            if (!dialogueBox[pos].activeSelf && interacted)
            {
                EndDialogue();
            }

            else if (!interacted && Input.GetKeyDown(KeyCode.E))
            {
                UpdateDialogue();
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
        dialogueBox[pos].SetActive(true);
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
        
        if(pos == 1) // after retrieved mail
        {
            spriteRenderer.sprite = sprite;
            dadDialogue3 = true;
            Invoke("ObtainedMail", 1.0f);
        }
    }

    private void UpdateDialogue()
    {
        if (dadInteract.GetMailboxDialogue1 && pos == 0)    // first interaction with mail
        {
            pos = 1;
        }

        else if (pos == 1)                                  // retrieve mail
        {
            pos = 2;
        }
    }

    private void ObtainedMail()
    {
        mailPanel.gameObject.SetActive(true);
        Invoke("UnobtainedMail", 2.0f);
    }

    private void UnobtainedMail()
    {
        letterAnimator.SetBool("ObtainedLetter", true);
    }
}
