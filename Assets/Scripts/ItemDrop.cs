using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : PickUp, IInteractable {
    [SerializeField] List<DropData> droppedItems = new List<DropData>();
    bool itemPickedUp;

    public void Init(List<DropData> dropData) {
        droppedItems = dropData;
    }

    public void OnInteract() {
        OnPickup();
    }

    public override void OnPickup() {
        if (itemPickedUp) return;

        List<DropData> pickedUp = new List<DropData>();

        foreach (var item in droppedItems) {
            if (InventorySystem.Instance.AddItem(item.item, item.amount)) {
                pickedUp.Add(item);
                //UIManager.Instance.ShowPickupInfo(item.item, item.amount);
            }
            else
                break;
        }

        for (int i = 0; i < pickedUp.Count; i++)
            droppedItems.Remove(pickedUp[i]);

        pickedUp.Clear();

        if (droppedItems.Count == 0) {
            Destroy(gameObject);
        }

        SoundManager.Instance.PlaySound(Vector3.zero, "ItemPickup");
    }


}
