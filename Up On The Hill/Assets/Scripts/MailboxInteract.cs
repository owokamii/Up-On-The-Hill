using UnityEngine;

public class MailboxInteract : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;

    //private bool inRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //inRange = true;
        speechBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //inRange = false;
        speechBubble.SetActive(false);
    }
}
