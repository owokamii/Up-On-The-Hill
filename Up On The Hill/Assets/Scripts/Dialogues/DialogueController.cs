using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator cinematicAnimator;
    [SerializeField] private TMP_Text NPCDialogueText;
    [SerializeField] private Image icon;
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTMP_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if(paragraphs.Count == 0)
        {
            if(!conversationEnded) 
            {
                StartConversation(dialogueText);
            }
            else if(conversationEnded && !isTyping)
            {
                EndConversation();
                return;
            }
        }

        if(!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        else
        {
            FinishParagraphEarly();
        }

        if(paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        StartCinematic();

        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        paragraphs.Clear();
        conversationEnded = false;

        EndCinematic();

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTMP_ALPHA);
            NPCDialogueText.text = displayedText;

            UpdateIconPosition();

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }

    private void UpdateIconPosition()
    {
        if (icon != null)
        {
            RectTransform textRT = NPCDialogueText.rectTransform;
            RectTransform iconRT = icon.rectTransform;

            // Calculate the position of the character icon relative to the text box width
            float textWidth = NPCDialogueText.preferredWidth;
            float iconOffset = iconRT.sizeDelta.x / 2f; // Adjust this value based on your icon's size

            iconRT.anchorMin = new Vector2(0.5f, 0.5f);
            iconRT.anchorMax = new Vector2(0.5f, 0.5f);
            iconRT.pivot = new Vector2(0.5f, 0.5f);
            iconRT.anchoredPosition = new Vector2(-textWidth / 2f - iconOffset, 0f);
        }
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);

        NPCDialogueText.text = p;

        isTyping = false;
    }

    private void StartCinematic()
    {
        playerController.enabled = false;
        cinematicAnimator.SetBool("Cinematic", true);
    }

    private void EndCinematic()
    {
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnablePlayerController", 2);
    }

    private void EnablePlayerController()
    {
        playerController.enabled = true;
    }
}
