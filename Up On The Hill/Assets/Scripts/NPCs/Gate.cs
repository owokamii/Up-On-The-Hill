using UnityEngine;

public class Gate : NPC, ITalkable
{
    [SerializeField] private Dad dad;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private DialogueController gateDialogueController;

    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;

    private bool interactedGate;
    private bool obtainedKey = true;
    private bool gateUnlocked;

    private int pos = 0;

    public override void Interact()
    {
        EventGateUnlocked();
        EventGateOpened();
        EventGateLocked();
        Talk(dialogueText[pos]);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    public void EventGateLocked()   // done
    {
        if (!interactedGate)
        {
            dad.GetInteractedGate = true;
            interactedGate = true;
        }
    }

    public void EventGateUnlocked()
    {
        //obtainedKey = dad.GetObtainedKey;

        if(obtainedKey && !gateUnlocked)
        {
            pos++;
            gateUnlocked = true;
        }
    }

    public void EventGateOpened()
    {
        if(gateUnlocked)
        {
            boxCollider.enabled = false;
            //enabled = false;
        }
    }
}