using UnityEngine;

public class GateInteract : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    private bool inRange = false;

    private void Update()
    {
        if(inRange)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                boxCollider.enabled = false;
                spriteRenderer.sprite = sprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        speechBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        speechBubble.SetActive(false);
    }
}
