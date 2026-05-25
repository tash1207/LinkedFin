using UnityEngine;

[CreateAssetMenu]
public class ConnectionSO : ScriptableObject
{
    public string connName;
    public string jobTitle;
    public string lookingFor;
    public Sprite portrait;

    [Header("Correct Fintroduction Outcomes")]
    public DialogueAction[] actionsToFire;
}
