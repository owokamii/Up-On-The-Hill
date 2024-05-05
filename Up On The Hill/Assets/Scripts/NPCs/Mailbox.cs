using UnityEngine;

public class Mailbox : NPC, ITalkable
{
    [SerializeField] private Dad dad;

    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private bool interactedGateLocked = false;
    [SerializeField] private bool interactedDad = false;
    [SerializeField] private bool obtainedMail = false;
    [SerializeField] private bool interactedMailboxEventEnded = false;

    [SerializeField] private int pos = 0;

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

    public void EventGateLocked() // done
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

    public void EventObtainedMail()
    {
        if (!interactedMailboxEventEnded && obtainedMail)
        {
            pos++;
            interactedMailboxEventEnded = true;
        }
    }
}