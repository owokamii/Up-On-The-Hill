using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    //AudioManager audioManager;

    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Image icon;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.05f;

    private int index;
    private bool sentenceEnded = false;

    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        textComponent.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && sentenceEnded)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = string.Empty;
                UpdateIconPosition();
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedStartDialogue(2.0f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator DelayedStartDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartDialogue();
    }

    private void StartDialogue()
    {
        index = 0;
        icon.enabled = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            UpdateIconPosition();
            //audioManager.PlaySFX(audioManager.sfx[0]);
            yield return new WaitForSeconds(textSpeed);
        }

        sentenceEnded = true;
    }

    private void NextLine()
    {
        sentenceEnded = false;

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                icon.enabled = false;
                textComponent.text = string.Empty;
                gameObject.SetActive(false);
            }
        }
    }

    private void UpdateIconPosition()
    {
        if (icon != null)
        {
            RectTransform textRT = textComponent.rectTransform;
            RectTransform iconRT = icon.rectTransform;

            // Calculate the position of the character icon relative to the text box width
            float textWidth = textComponent.preferredWidth;
            float iconOffset = iconRT.sizeDelta.x / 2f; // Adjust this value based on your icon's size

            iconRT.anchorMin = new Vector2(0.5f, 0.5f);
            iconRT.anchorMax = new Vector2(0.5f, 0.5f);
            iconRT.pivot = new Vector2(0.5f, 0.5f);
            iconRT.anchoredPosition = new Vector2(-textWidth / 2f - iconOffset, 0f);
        }
    }
}