using UnityEngine;

[CreateAssetMenu]
public class GiveItemAction : DialogueAction
{
    public ItemSO itemSO;
    public override void ExecuteAction()
    {
        if (itemSO.itemName == "Pen")
        {
            Actions.OnGivePen();
        }
        else if (itemSO.itemName == "Seaweed")
        {
            Actions.OnGiveSeaweed();
        }
        Debug.Log($"Gave away {itemSO.itemName}!");
    }
}
