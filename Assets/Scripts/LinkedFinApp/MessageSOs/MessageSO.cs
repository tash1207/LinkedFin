using UnityEngine;

[CreateAssetMenu]
public class MessageSO : ScriptableObject
{
    public ConnectionSO connection;
    [TextArea]
    public string messageText;

    public DialogueAction[] actionsToFire;
}
