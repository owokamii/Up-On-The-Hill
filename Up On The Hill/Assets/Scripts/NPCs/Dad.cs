using UnityEngine;

public class Dad : NPC, ITalkable
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private bool interactedGateLocked = false;
    [SerializeField] private bool gateLockedEventEnded = false;

    [SerializeField] private bool interactedDad = false;
    [SerializeField] private bool dadEventEnded = false;

    [SerializeField] private bool obtainedMail = false;
    [SerializeField] private bool mailEventEnded = false;

    [SerializeField] private bool obtainedKey = false;
    [SerializeField] private bool keyEventEnded = false;

    [SerializeField] private int pos = 0;

    public bool GetInteractedGate { get => interactedGateLocked; set => interactedGateLocked = value; }
    public bool GetInteractedDad { get => interactedDad; }
    public bool GetObtainedMail { set => obtainedMail = value; }

    public override void Interact()
    {
        EventGateLocked();
        EventInteractDad();
        EventObtainedMail();
        Talk(dialogueText[pos]);
        EventObtainedKey();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    public void EventGateLocked() // done
    {
        if (interactedGateLocked && !gateLockedEventEnded)
        {
            pos++;
            gateLockedEventEnded = true;
        }
    }

    public void EventInteractDad() // done
    {
        if(interactedGateLocked && !interactedDad && !dadEventEnded)
        {
            interactedDad = true;
            dadEventEnded = true;
        }
    }

    public void EventObtainedMail()
    {
        if (obtainedMail && !mailEventEnded)
        {
            pos++;
            obtainedKey = true;
            mailEventEnded = true;
        }
    }

    public void EventObtainedKey()
    {
        if(obtainedKey && !keyEventEnded && mailEventEnded)
        {
            pos++;
            keyEventEnded = true;
        }
    }
}