using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    bool itemPickedUp;
    List<DropData> droppedItems = new List<DropData>();

    public void Init(List<DropData> dropData) {
        droppedItems = dropData;
    }

    public void OnPickup() {
        if (itemPickedUp) return;

        List<DropData> pickedUp = new List<DropData>();

        foreach (var item in droppedItems) {
            if(InventorySystem.Instance.AddItem(item.item, item.amount))
                pickedUp.Add(item);
            else
                break;
        }

        for (int i = 0; i < pickedUp.Count; i++)
            droppedItems.Remove(pickedUp[i]);

        pickedUp.Clear();

        if (droppedItems.Count == 0) {
            Destroy(gameObject);
        }
    }


}
