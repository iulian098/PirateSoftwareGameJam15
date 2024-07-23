using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoSingleton<HotbarManager>
{
    [SerializeField] UI_HotbarSlot[] slots;

    ItemData[] items;

    UI_Slot selectedSlot;
    int selectedSlotIndex = -1;

    UI_Slot overSlot;
    int overSlotIndex = -1;

    private void Start() {
        items = new ItemData[slots.Length];
        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            slots[i].SetItem(items[i]);
        }
    }

    public void SetOverSlot(UI_Slot slot) {
        overSlot = slot;
        overSlotIndex = slot != null ? slot.SlotIndex : -1;
    }

    public void Drag(UI_Slot slot) {
        selectedSlot = slot;
        selectedSlotIndex = slot.SlotIndex;


    }

    public void Drop() {
        if(selectedSlot == null) {
            selectedSlot = InventorySystem.Instance.SelectedSlot;
            selectedSlotIndex = InventorySystem.Instance.SelectedSlotIndex;
            if (overSlot != null && selectedSlot.Item.Type == Enum_ItemType.Equipment) {
                overSlot.SetItem(selectedSlot.Item);
                items[overSlotIndex] = selectedSlot.Item;

                Clear();
            }
            return;
        }else if(selectedSlot != null && overSlot == null) {
            selectedSlot.SetItem(null);
            items[selectedSlotIndex] = null;
        }

        if (selectedSlot == null || overSlot == null) return;

        ItemData aux = items[overSlotIndex];

        items[overSlotIndex] = items[selectedSlotIndex];
        items[selectedSlotIndex] = aux;

        overSlot.SetItem(items[overSlotIndex]);
        selectedSlot.SetItem(items[selectedSlotIndex]);

        Clear();
    }

    void Clear() {
        selectedSlot = null;
        selectedSlotIndex = -1;

        overSlot = null;
        overSlotIndex = -1;
    }

}
