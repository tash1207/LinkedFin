using Unity.VectorGraphics;
using UnityEngine;

[CreateAssetMenu]
public class TransportAction : DialogueAction
{
    public override void ExecuteAction()
    {
        FindFirstObjectByType<SceneTransporter>().TransportToShip();
    }
}
