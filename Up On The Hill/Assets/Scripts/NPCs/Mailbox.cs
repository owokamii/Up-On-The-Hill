using UnityEngine;

public class Mailbox : NPC, ITalkable
{
    [SerializeField] private Dad dad;

    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    private bool interactedGateLocked = false;
    private bool interactedDad = false;
    private bool obtainedMail = false;
    private bool interactedMailboxEventEnded = false;

    private int pos = 0;

    public override void Interact()
    {
        EventGateLocked();
        Talk(dialogueText[pos]);
        EventObtainedMail();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    public void EventGateLocked()   // done
    {
        interactedGateLocked = dad.GetInteractedGate;
        interactedDad = dad.GetInteractedDad;

        if (interactedGateLocked && interactedDad && !obtainedMail)
        {
            pos++;
            dad.GetObtainedMail = true;
            interactedGateLocked = true;
            obtainedMail = true;
        }
    }

    public void EventObtainedMail() // done
    {
        if (!interactedMailboxEventEnded && obtainedMail)
        {
            pos++;
            interactedMailboxEventEnded = true;
        }
    }
}