using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image nextIndicator;

    [Header("Parameters")]
    [SerializeField] private float targetAlpha;
    [SerializeField] private float transitionDuration;
    [SerializeField] private bool startCantMove;
    [SerializeField] private float nextDelay = 0.3f;

    private Coroutine fadeCoroutine;
    private bool disableSelf;
    private bool coroutineEnded;
    private bool canPress = false;

    private void Start()
    {
        if(startCantMove)
        {
            playerController.enabled = false;
        }

        FadeTo(targetAlpha);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && coroutineEnded && canPress)
        {
            coroutineEnded = false;
            disableSelf = true;
            FadeTo(-targetAlpha);
        }
    }

    public void FadeTo(float targetAlphaValue)
    {
        float startAlpha = canvasGroup.alpha;

        fadeCoroutine = StartCoroutine(FadeCoroutine(startAlpha, targetAlphaValue));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float targetAlphaValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlphaValue, t);

            canvasGroup.alpha = currentAlpha;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        canvasGroup.alpha = targetAlphaValue;

        if (!coroutineEnded && targetAlphaValue == 1f)
        {
            nextIndicator.enabled = true;
            coroutineEnded = true;
            yield return new WaitForSeconds(nextDelay);
            canPress = true;
        }

        if (disableSelf)
        {
            playerController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}