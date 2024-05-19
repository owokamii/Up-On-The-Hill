using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private MonoBehaviour NPC;

    [Header("Game Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource footstepAudioSource;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] flowers;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed;

    [Header("Audio")]
    [SerializeField] private AudioClip[] footstepAudioClip;

    private int flowerNum;
    private int pos;
    private float xAxis;
    private bool canMove = true;
    private bool isFacingRight = true;

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
        StopFootstepAudio();
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

        if (xAxis != 0 && gameObject.activeSelf)
        {
            PlayFootstepAudio();
        }
        else
        {
            StopFootstepAudio();
        }

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

    private void PlayFootstepAudio()
    {
        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.clip = footstepAudioClip[pos];
            footstepAudioSource.loop = true;
            footstepAudioSource.Play();
        }
    }

    private void StopFootstepAudio()
    {
        if (footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }

    }
    
    private void ChangeFootstepAudio()
    {
        StopFootstepAudio();
        PlayFootstepAudio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Indoor"))
        {
            pos = 1;

            ChangeFootstepAudio();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Indoor"))
        {
            pos = 0;

            ChangeFootstepAudio();
        }
    }
}