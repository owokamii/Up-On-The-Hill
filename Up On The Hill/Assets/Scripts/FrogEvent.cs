using UnityEngine;

public class FrogEvent : MonoBehaviour
{
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Animator frogAnimator;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject racoon;
    [SerializeField] private GameObject frog;
    [SerializeField] private GameObject frogPanel;
    [SerializeField] private Animator frogPanelAnimator;

    private bool interacted = false;

    public bool triggerEvent = false;

    private void Update()
    {
        if (!racoon)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !interacted)
            {
                Debug.Log("racconn!");
                StartDialogue();

            }
            else if (!dialogueBox.activeSelf && interacted)
            {
                Debug.Log("racconDASn!");
                EndDialogue();
            }
        }

        if(triggerEvent)
        {
            ObtainedFrog();
        }
    }

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
        Invoke("ObtainedFrog", 4.0f);
    }

    private void DelayAnimation()
    {
        frogAnimator.SetBool("EventStart", true);
    }

    private void FrogAppear()
    {
        frog.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(frog);
            Invoke("ObtainedFrog", 1.0f);
        }
    }

    private void ObtainedFrog()
    {
        frogPanel.gameObject.SetActive(true);
        Invoke("UnobtainedFrog", 2.0f);
    }

    private void UnobtainedFrog()
    {
        frogPanelAnimator.SetBool("ObtainedFrog", true);
        DestroyFrog();

    }

    private void DestroyFrog()
    {
        Destroy(frog);
    }
}
