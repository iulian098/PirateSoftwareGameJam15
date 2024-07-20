using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem : MonoSingleton<InventorySystem>
{
    [SerializeField] UI_Slot[] slots;

    [SerializeField] ItemData testItemData;

    ItemData[] items;

    UI_Slot selectedSlot;
    int selectedSlotIndex;

    UI_Slot overSlot;
    int overSlotIndex;

    private void Start() {
        items = new ItemData[slots.Length];
        items[0] = testItemData;
        items[slots.Length - 1] = testItemData;

        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            slots[i].SetItem(items[i]);
        }
    }

    public void SetOverSlot(UI_Slot slot) {
        overSlot = slot;
        overSlotIndex = slot.SlotIndex;
    }

    public void Drag(UI_Slot slot) {
        selectedSlot = slot;
        selectedSlotIndex = slot.SlotIndex;

        if(slot.Item != null) {

        }

        Debug.Log("[Inventory] On Item Drag " + slot.name);

    }

    public void Drop() {
        if (selectedSlot == null || overSlot == null) return;

        ItemData aux = items[overSlotIndex];

        items[overSlotIndex] = items[selectedSlotIndex];
        items[selectedSlotIndex] = aux;

        overSlot.SetItem(items[overSlotIndex]);
        selectedSlot.SetItem(items[selectedSlotIndex]);

        Debug.Log($"[Inventory] On Item Dropped {selectedSlot.name} - {overSlot.name}");
    }

}
