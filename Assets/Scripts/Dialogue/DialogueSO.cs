using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public DialogueSpeakers[] speakers;

    [Tooltip("Only needed if Random is selected as the speaker name")]
    [Header("Random Speaker Info")]
    public string randomSpeakerName;
    public Sprite randomSpeakerPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    [Tooltip("The words that appear on the buttons")]
    public string[] options;

    public DialogueSO option0;
    public DialogueSO option1;
    public DialogueSO option2;
    public DialogueSO option3;
}
