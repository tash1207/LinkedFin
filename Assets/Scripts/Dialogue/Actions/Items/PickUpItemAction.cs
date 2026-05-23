using UnityEngine;

[CreateAssetMenu]
public class PickUpItemAction : DialogueAction
{
    public ItemSO itemSO;
    public override void ExecuteAction()
    {
        if (itemSO.itemName == "Pen")
        {
            Actions.OnPickUpPen();
        }
        else if (itemSO.itemName == "Seaweed")
        {
            Actions.OnPickUpSeaweed();
        }
        Debug.Log($"Picked up {itemSO.itemName}!");
    }
}
