using System.Collections;
using UnityEngine;

public class UISquish : MonoBehaviour
{
    public Vector3 squishedScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float squishDuration = 0.1f;
    public float bounceBackDuration = 0.2f;

    private Vector3 originalScale;
    private bool isAnimating = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Trigger the squish effect per press, ignoring if another animation is ongoing
        if (Input.GetButtonDown("Interact") && !isAnimating)
        {
            StopAllCoroutines();
            StartCoroutine(SquishAndBounceBack());
        }
    }

    private IEnumerator SquishAndBounceBack()
    {
        isAnimating = true;
        float elapsedTime = 0;

        // Squish phase
        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = squishedScale;

        // Reset elapsed time for bounce-back phase
        elapsedTime = 0;

        // Bounce-back phase
        while (elapsedTime < bounceBackDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / bounceBackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false; // Animation finished, allow next press
    }
}
