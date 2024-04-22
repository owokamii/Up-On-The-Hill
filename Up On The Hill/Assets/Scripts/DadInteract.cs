using UnityEngine;

public class DadInteract : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isInteracted = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isInteracted)
            {
                animator.SetBool("Interacted", true);
                isInteracted = true;
            }
            else
            {
                animator.SetBool("Interacted", false);
                isInteracted = false;
            }
        }
    }
}
