using UnityEngine;

[CreateAssetMenu]
public class SetDialogueStep : DialogueAction
{
    public SpeakerSO speakerSO;
    public DialogueSO dialogueSO;
    public override void ExecuteAction()
    {
        Actions.SetDialogStep(speakerSO, dialogueSO);
    }
}
