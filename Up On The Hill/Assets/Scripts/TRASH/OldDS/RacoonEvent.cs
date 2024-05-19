/*using UnityEngine;

public class RacoonEvent : MonoBehaviour
{
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private Animator racoonAnimator;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject flower5;
    [SerializeField] private GameObject racoon;

    private void ActivateDialogue()
    {
        dialogueBox.SetActive(true);
    }

    private void StartDialogue()
    {
        interacted = true;
        Invoke("DelayAnimation", 1.0f);
        cinematicAnimator.SetBool("Cinematic", true);   // black bars enable
        Invoke("ActivateDialogue", 0);               // dialogue starts
    }

    private void EndDialogue()
    {
        interacted = false;
        cinematicAnimator.SetBool("Cinematic", false);      // black bars disable
    }

    private void DelayAnimation()
    {
        racoonAnimator.SetBool("EventStart", true);
    }

    private void RacoonAppear()
    {
        racoon.SetActive(true);
    }

    private void DestroyRacoon()
    {
        Destroy(racoon);
    }
}
*/