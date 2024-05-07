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
    private bool conversationStarted;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTMP_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public bool GetConversationEnded { get => conversationEnded; }

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

        if (paragraphs.Count == 0)
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
        icon.enabled = false;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        if(conversationStarted)
        {
            yield return new WaitForSeconds(2.0f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            UpdateIconPosition();

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTMP_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }

    private void UpdateIconPosition()
    {
        icon.enabled = true;

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
        if(!conversationStarted)
        {
            StopCoroutine(typeDialogueCoroutine);

            NPCDialogueText.text = p;
            UpdateIconPosition();

            isTyping = false;
        }
    }

    private void StartCinematic()
    {
        conversationStarted = true;
        playerController.enabled = false;
        cinematicAnimator.SetBool("Cinematic", true);
        Invoke("CinematicPlaying", 2.0f);
    }

    private void CinematicPlaying()
    {
        conversationStarted = false;
    }

    private void EndCinematic()
    {
        cinematicAnimator.SetBool("Cinematic", false);
        Invoke("EnablePlayerController", 2.0f);
    }

    private void EnablePlayerController()
    {
        playerController.enabled = true;
    }
}
