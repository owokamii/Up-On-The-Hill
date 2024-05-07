/*using UnityEngine;

public abstract class N : MonoBehaviour, II
{
    [SerializeField] private SpriteRenderer speechBubble;

    private void Update()
    {
        if (speechBubble && Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speechBubble.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speechBubble.enabled = false;
    }

    public abstract void Interact();
}
*/