using UnityEngine;

[CreateAssetMenu]
public class UpdateProfileAction : DialogueAction
{
    public ConnectionSO connectionSO;
    public ConnectionSO newConnectionSO;
    public override void ExecuteAction()
    {
        Actions.UpdateProfile(connectionSO, newConnectionSO);
        Debug.Log($"Updated {connectionSO.connName}");
    }
}
