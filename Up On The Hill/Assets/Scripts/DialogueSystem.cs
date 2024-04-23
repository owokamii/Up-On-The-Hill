using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    //AudioManager audioManager;

    [SerializeField] private TextMeshProUGUI textComponent;
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
        if (Input.GetKeyDown(KeyCode.Space) && sentenceEnded)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = string.Empty;
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
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                textComponent.text = string.Empty;
                gameObject.SetActive(false);
            }
        }
    }
}