using System.Collections;
using UnityEngine;

public class RandomAnimationAI : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private Animator animator;

    [Header("Animation Settings")]
    [SerializeField] private string[] animationStates;
    [SerializeField] private float idleTime = 1.0f;

    private void Start()
    {
        if (animationStates.Length > 0)
        {
            StartCoroutine(PlayRandomAnimation());
        }
        else
        {
            Debug.LogWarning("No animations specified.");
        }
    }

    private IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, animationStates.Length);
            string selectedAnimation = animationStates[randomIndex];

            animator.Play(selectedAnimation);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            yield return new WaitForSeconds(idleTime);
        }
    }
}