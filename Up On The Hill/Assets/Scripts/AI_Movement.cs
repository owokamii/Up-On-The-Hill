using System.Collections;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float minWalkTime = 1f;
    [SerializeField] private float maxWalkTime = 3f;
    [SerializeField] private float minIdleTime = 3f;
    [SerializeField] private float maxIdleTime = 5f;
    [SerializeField] private float walkSpeed = 0.5f;

    private Animator animator;
    private Vector3 startingPosition;
    private bool movingRight;
    private float walkTimer;
    private float idleTimer;
    private bool isIdle;
    private bool isFacingRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();

        startingPosition = transform.position;
        PickRandomDirection();
        walkTimer = Random.Range(minWalkTime, maxWalkTime);
    }

    private void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        walkTimer -= Time.deltaTime;
        animator.SetBool("Walking", true);

        HandleFlip();

        if (movingRight)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
            if (transform.position.x >= startingPosition.x + patrolDistance)
            {
                HandleFlip();
                isIdle = true;
                idleTimer = Random.Range(minIdleTime, maxIdleTime);
            }
        }
        else
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
            if (transform.position.x <= startingPosition.x - patrolDistance)
            {
                HandleFlip();
                isIdle = true;
                idleTimer = Random.Range(minIdleTime, maxIdleTime);
            }
        }

        if (walkTimer <= 0)
        {
            isIdle = true;
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
        }
    }

    private void Idle()
    {
        idleTimer -= Time.deltaTime;
        animator.SetBool("Walking", false);

        if (idleTimer <= 0)
        {
            PickRandomDirection();
            walkTimer = Random.Range(minWalkTime, maxWalkTime);
            isIdle = false;
        }
    }

    private void HandleFlip()
    {
        if ((movingRight && !isFacingRight) || (!movingRight && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
    }

    private void PickRandomDirection()
    {
        movingRight = (Random.Range(0, 2) == 0);
    }
}
