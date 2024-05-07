using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private MonoBehaviour NPC;

    [Header("Game Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] flowers;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed;

    private float xAxis;
    public bool canMove;
    private bool isFacingRight = true;

    public bool GetCanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        canMove = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        animator.SetFloat("xAxis", Mathf.Abs(0));
        rigidBody.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (canMove)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xAxis", Mathf.Abs(xAxis));
        rigidBody.velocity = new Vector2(xAxis * movementSpeed, rigidBody.velocity.y);

        if (xAxis < 0 && isFacingRight || xAxis > 0 && !isFacingRight)
        {
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