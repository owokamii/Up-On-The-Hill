using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform player;
    [SerializeField] private Transform indoorCamera;

    [SerializeField] private float cameraZoomIn;
    [SerializeField] private float cameraZoomOut;
    [SerializeField] private float transitionDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCamera.Follow = indoorCamera;
        StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomOut));
        //playerCamera.m_Lens.OrthographicSize = cameraZoomOut;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCamera.Follow = player;
        StartCoroutine(ZoomCoroutine(playerCamera.m_Lens.OrthographicSize, cameraZoomIn));
        //playerCamera.m_Lens.OrthographicSize = cameraZoomIn;
    }

    private IEnumerator ZoomCoroutine(float startSize, float targetSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            float newSize = Mathf.Lerp(startSize, targetSize, t);
            playerCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.m_Lens.OrthographicSize = targetSize;
    }
}


/*using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueSystems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Image characterIcon; // Reference to the character icon image
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.05f;

    private int index;
    private bool sentenceEnded = false;

    private void Start()
    {
        textComponent.text = string.Empty;
        StartCoroutine(DelayedStartDialogue(2.0f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sentenceEnded)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                UpdateIconPosition(); // Update icon position when the line is completed
            }
        }
    }

    private IEnumerator DelayedStartDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartDialogue();
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            UpdateIconPosition(); // Update icon position for each character typed
            yield return new WaitForSeconds(textSpeed);
        }

        sentenceEnded = true;
    }

    private void StartDialogue()
    {
        index = 0;
        characterIcon.enabled = true;
        StartCoroutine(TypeLine());
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
            characterIcon.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void UpdateIconPosition()
    {
        if (characterIcon != null)
        {
            RectTransform textRT = textComponent.rectTransform;
            RectTransform iconRT = characterIcon.rectTransform;

            // Calculate the position of the character icon relative to the text box width
            float textWidth = textComponent.preferredWidth;
            float iconOffset = iconRT.sizeDelta.x / 2f; // Adjust this value based on your icon's size

            iconRT.anchorMin = new Vector2(0.5f, 0.5f);
            iconRT.anchorMax = new Vector2(0.5f, 0.5f);
            iconRT.pivot = new Vector2(0.5f, 0.5f);
            iconRT.anchoredPosition = new Vector2(-textWidth / 2f - iconOffset, 0f);
        }
    }
}*/