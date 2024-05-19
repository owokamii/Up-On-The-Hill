using UnityEngine.InputSystem;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer speechBubble;

    private Transform playerTransform;

    private const float INTERACT_DISTANCE = 4f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame && IsWithinInteractDistance())
        {
            Interact();
        }

        if(speechBubble.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            speechBubble.gameObject.SetActive(false);
        }
        else if(!speechBubble.gameObject.activeSelf && IsWithinInteractDistance())
        {
            speechBubble.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if(Vector2.Distance(playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
