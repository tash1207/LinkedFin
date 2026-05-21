using UnityEngine;

[CreateAssetMenu]
public class AddConnectionAction : DialogueAction
{
    public ConnectionSO connectionSO;
    public override void ExecuteAction()
    {
        Actions.OnConnectionMade(connectionSO);
        Debug.Log($"Connected with {connectionSO.connName}!");
    }
}
