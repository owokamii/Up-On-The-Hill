using UnityEngine;

public class F_Movement : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private SpriteRenderer speechBubble;
    [SerializeField] private Animator frogAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject dialogueBox;

    [Header("Parameters")]
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private float patrolDistance = 2.0f;
    [SerializeField] private float idleTime = 5.0f;

    private float initialPosition;
    private float idleTimer;
    private bool isFacingRight;
    private bool patrol;

    private void Awake()
    {
        patrol = true;
        initialPosition = transform.position.x;
    }

    private void OnDisable()
    {
        frogAnimator.SetFloat("xAxis", Mathf.Abs(0));
    }

    private void Update()
    {
        if (patrol)
        {
            Patrol();
        }
        else
        {
            Idle();
        }
    }

    private void Patrol()
    {
        frogAnimator.SetFloat("xAxis", Mathf.Abs(patrolSpeed));

        float targetPosition = isFacingRight ? initialPosition + patrolDistance : initialPosition - patrolDistance;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition, transform.position.y), patrolSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - targetPosition) < 0.1f)
        {
            patrol = false;
            idleTimer = 0.0f;
        }
    }

    private void Idle()
    {
        frogAnimator.SetFloat("xAxis", 0f);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime)
        {
            idleTimer = 0.0f;
            patrol = true;
            HandleFlip();
        }
    }

    private void HandleFlip()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}