using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource footstepAudioSource;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] flowers;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintSpeedMultiplier = 1.5f;
    [SerializeField] private float animationSpeedFactor = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioClip[] leftFootstepAudioClips;
    [SerializeField] private AudioClip[] rightFootstepAudioClips;
    [SerializeField] private AudioClip[] indoorLeftFootstepAudioClips;
    [SerializeField] private AudioClip[] indoorRightFootstepAudioClips;

    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D frictionMaterial;
    [SerializeField] private PhysicsMaterial2D smoothMaterial;

    private int flowerNum;
    private float xAxis;
    private bool canMove = true;
    private bool isFacingRight = true;
    private bool isLeftFootNext = true;
    private bool isIndoors = false;

    public int GetFlowerNum { get => flowerNum; set => flowerNum = value; }
    public bool GetCanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        footstepAudioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        animator.SetFloat("xAxis", Mathf.Abs(0));
        rigidBody.velocity = Vector2.zero;
    }

    private void FixedUpdate()
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
        float currentSpeed = movementSpeed;

        if (Input.GetButton("Sprint"))
        {
            currentSpeed *= sprintSpeedMultiplier;
        }

        if (rigidBody.bodyType == RigidbodyType2D.Dynamic)
        {
            rigidBody.velocity = new Vector2(xAxis * currentSpeed, rigidBody.velocity.y);
        }

        animator.speed = Mathf.Abs(currentSpeed) * animationSpeedFactor;
        animator.SetFloat("xAxis", Mathf.Abs(xAxis));

        rigidBody.sharedMaterial = xAxis != 0 ? smoothMaterial : frictionMaterial;

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

    // This method will be called via animation events
    public void PlayFootstepAudio()
    {
        AudioClip[] footstepClips = isLeftFootNext
            ? (isIndoors ? indoorLeftFootstepAudioClips : leftFootstepAudioClips)
            : (isIndoors ? indoorRightFootstepAudioClips : rightFootstepAudioClips);

        int randomIndex = Random.Range(0, footstepClips.Length);
        footstepAudioSource.clip = footstepClips[randomIndex];
        footstepAudioSource.Play();

        isLeftFootNext = !isLeftFootNext;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Indoor"))
        {
            isIndoors = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Indoor"))
        {
            isIndoors = false;
        }
    }
}
