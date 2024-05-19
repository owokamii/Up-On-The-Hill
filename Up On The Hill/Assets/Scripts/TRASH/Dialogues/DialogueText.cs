using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
{
    public Sprite icon;  // not done

    [TextArea(3, 10)]
    public string[] paragraphs;
}
