using UnityEngine;

public class Dad : NPC, ITalkable
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private bool interactedGateLocked = false;
    [SerializeField] private bool gateLockedEventEnded = false;

    private bool interactedDad;
    private bool dadEventEnded;

    private bool obtainedMail;
    private bool mailEventEnded;

    private bool obtainedKey;
    private bool keyEventEnded;

    private int pos = 0;

    public bool GetInteractedGate { get => interactedGateLocked; set => interactedGateLocked = value; }
    public bool GetInteractedDad { get => interactedDad; }
    public bool GetObtainedMail { set => obtainedMail = value; }
    public bool GetObtainedKey { get => obtainedKey; }

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

    public void EventGateLocked()   // done
    {
        if (interactedGateLocked && !gateLockedEventEnded)
        {
            pos++;
            gateLockedEventEnded = true;
        }
    }

    public void EventInteractDad()  // done
    {
        if(interactedGateLocked && !interactedDad && !dadEventEnded)
        {
            interactedDad = true;
            dadEventEnded = true;
        }
    }

    public void EventObtainedMail() // done
    {
        if (obtainedMail && !mailEventEnded)
        {
            pos++;
            obtainedKey = true;
            mailEventEnded = true;
        }
    }

    public void EventObtainedKey()  // done
    {
        if(obtainedKey && !keyEventEnded && mailEventEnded)
        {
            pos++;
            keyEventEnded = true;
        }
    }
}