using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public bool isAutomatic;
    public bool isOneTime;
    public DialogueSpeakers[] speakers;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    [Tooltip("The words that appear on the buttons")]
    public string[] options;

    public DialogueSO option0;
    public DialogueSO option1;
    public DialogueSO option2;
    public DialogueSO option3;

    [Header("Conditional Requirements (Optional)")]
    public SpeakerSO[] requiredNPCs;

    [Tooltip("Only needed if Random is selected as the speaker name")]
    [Header("Random Speaker Info")]
    public string randomSpeakerName;
    public Sprite randomSpeakerPortrait;

    public bool IsConditionMet()
    {
        if (requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                if (DialogueHistoryTracker.Instance.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
