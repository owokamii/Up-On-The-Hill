using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DS : MonoBehaviour
{
    [SerializeField] private TMP_Text NPCDialogue;
    [SerializeField] private Image NPCIcon;
    [SerializeField] private Image indicatorIcon;

    [TextArea(2, 10)]
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private float invokeTime = 2.0f;
    [SerializeField] private bool disableAuto;

    private int index;
    private bool lineEnded;

    private void OnEnable()
    {
        indicatorIcon.enabled = false;

        if (disableAuto)
        {
            StartCoroutine(StartDialogue());
        }
        else
        {
            NextLine();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(invokeTime);
        NPCIcon.enabled = true;
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && lineEnded)
        {
            if (NPCDialogue.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private void NextLine()
    {
        lineEnded = false;
        indicatorIcon.enabled = false;

        if (index < lines.Length - 1)
        {
            index++;
            NPCDialogue.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            index = 0;
            NPCIcon.enabled = false;
            NPCDialogue.text = string.Empty;
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        if (!(lines.Length == 0))
        {
            foreach (char c in lines[index].ToCharArray())
            {
                NPCDialogue.text += c;
                UpdateNPCIconPosition();
                yield return new WaitForSeconds(textSpeed);
            }

            lineEnded = true;
            indicatorIcon.enabled = true;
        }
    }

    private void UpdateNPCIconPosition()
    {
        if (NPCIcon != null)
        {
            RectTransform iconRT = NPCIcon.rectTransform;

            float textWidth = NPCDialogue.preferredWidth;
            float iconOffset = iconRT.sizeDelta.x / 2f;

            iconRT.anchorMin = new Vector2(0.5f, 0.5f);
            iconRT.anchorMax = new Vector2(0.5f, 0.5f);
            iconRT.pivot = new Vector2(0.5f, 0.5f);
            iconRT.anchoredPosition = new Vector2(-textWidth / 2f - iconOffset, 0f);
        }
    }
}
