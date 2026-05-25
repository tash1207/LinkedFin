using UnityEngine;

[CreateAssetMenu]
public class EndGameAction : DialogueAction
{
    public override void ExecuteAction()
    {
        Actions.OnGameOver();
    }
}
