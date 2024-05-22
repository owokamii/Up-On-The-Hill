using System.Collections;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private PlayerController playerController;

    [Header("Parameters")]
    [SerializeField] private float targetAlpha;
    [SerializeField] private float transitionDuration;
    [SerializeField] private bool startCantMove;

    private Coroutine fadeCoroutine;
    private bool disableSelf;
    private bool coroutineEnded;

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
        if(Input.GetButtonDown("Interact"))
        {
            if(coroutineEnded)
            {
                coroutineEnded = false;
                disableSelf = true;
                FadeTo(-targetAlpha);
            }
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

        if(!coroutineEnded)
        {
            coroutineEnded = true;
        }

        if (disableSelf)
        {
            playerController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}