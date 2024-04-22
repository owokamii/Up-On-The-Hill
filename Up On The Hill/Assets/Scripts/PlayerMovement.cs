using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    [SerializeField] private float movementSpeed;

    private float xAxis;
    private bool isFacingRight = true;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
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