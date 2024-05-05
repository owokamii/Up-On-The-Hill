using UnityEngine;

public class Gate : NPC, ITalkable
{
    [SerializeField] private Dad dad;

    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private bool interactedGate = false;

    [SerializeField] private int pos = 0;

    public override void Interact()
    {
        EventGateLocked();
        Talk(dialogueText[pos]);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    public void EventGateLocked()
    {
        if (!interactedGate) // done
        {
            dad.GetInteractedGate = true;
            interactedGate = true;
        }
    }
}