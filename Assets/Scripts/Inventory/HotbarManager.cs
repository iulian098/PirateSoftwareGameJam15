using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoSingleton<HotbarManager>
{
    [SerializeField] UI_HotbarSlot[] slots;
    [SerializeField] GameObject disabledObj;
    [SerializeField] ItemsContainer itemsContainer;
    [SerializeField] InventoryContainer inventoryContainer;
    ItemData[] items;

    UI_Slot selectedSlot;
    int selectedSlotIndex = -1;

    UI_Slot overSlot;
    int overSlotIndex = -1;

    ItemDragIcon ItemDragIcon => UIManager.Instance.ItemDragIcon;

    private void Start() {
        items = new ItemData[slots.Length];
        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            //slots[i].SetItem(items[i]);
            ItemData item = itemsContainer.GetItemByID(inventoryContainer.HotbarIDs[i]);
            if (item != null) {
                int amountIndex = inventoryContainer.ItemsIDs.IndexOf(item.ID);
                slots[i].SetItem(item, inventoryContainer.Amounts[amountIndex]);
            }
            else
                slots[i].SetItem(null);
        }
        
    }

    public void SetDisabled(bool disable) {
        disabledObj.SetActive(disable);
    }

    public void SetOverSlot(UI_Slot slot) {
        overSlot = slot;
        overSlotIndex = slot != null ? slot.SlotIndex : -1;
    }

    public void Drag(UI_Slot slot) {
        selectedSlot = slot;
        selectedSlotIndex = slot.SlotIndex;

        if (selectedSlot.Item == null) return;

        ItemDragIcon.Show(selectedSlot.Item.Icon);
    }

    public void Drop() {
        //itemDragObject.SetActive(false);
        ItemDragIcon.Hide();
        if (selectedSlot == null && overSlot != null) {
            selectedSlot = InventorySystem.Instance.SelectedSlot;
            selectedSlotIndex = InventorySystem.Instance.SelectedSlotIndex;
            if (selectedSlot != null && selectedSlot.Item != null && overSlot != null && selectedSlot.Item.Type == Enum_ItemType.Equipment) {

                int alreadyExistsIndex = inventoryContainer.HotbarIDs.IndexOf(selectedSlot.Item.ID);
                if (alreadyExistsIndex != -1) {
                    inventoryContainer.HotbarIDs[alreadyExistsIndex] = 0;
                    slots[alreadyExistsIndex].SetItem(null);
                }

                int itemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedSlot.Item.ID);
                if (itemIndex != -1) {
                    int itemAmount = inventoryContainer.Amounts[itemIndex];
                    overSlot.SetItem(selectedSlot.Item, itemAmount);
                }
                else
                    overSlot.SetItem(selectedSlot.Item);
                inventoryContainer.HotbarIDs[overSlotIndex] = selectedSlot.Item.ID;

                Clear();
            }
            return;
        }else if(selectedSlot != null && overSlot == null) {
            selectedSlot.SetItem(null);
            //items[selectedSlotIndex] = null;
            inventoryContainer.HotbarIDs[selectedSlotIndex] = 0;
            Clear();
        }

        if (selectedSlot == null || overSlot == null) return;

        int aux = inventoryContainer.HotbarIDs[overSlotIndex];
        inventoryContainer.HotbarIDs[overSlotIndex] = inventoryContainer.HotbarIDs[selectedSlotIndex];
        inventoryContainer.HotbarIDs[selectedSlotIndex] = aux;

        if (inventoryContainer.HotbarIDs[overSlotIndex] == 0)
            overSlot.SetItem(null);
        else {
            ItemData overSlotItem = itemsContainer.GetItemByID(inventoryContainer.HotbarIDs[overSlotIndex]);
            int overSlotItemIndex = inventoryContainer.ItemsIDs.IndexOf(overSlotItem.ID);
            if (overSlotItemIndex != -1)
                overSlot.SetItem(overSlotItem, inventoryContainer.Amounts[overSlotItemIndex]);
            else
                overSlot.SetItem(overSlotItem);
        }

        if (inventoryContainer.HotbarIDs[selectedSlotIndex] == 0)
            selectedSlot.SetItem(null);
        else {
            ItemData selectedSlotItem = itemsContainer.GetItemByID(inventoryContainer.HotbarIDs[selectedSlotIndex]);
            int selectedSlotItemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedSlotItem.ID);
            if (selectedSlotItemIndex != -1)
                selectedSlot.SetItem(selectedSlotItem, inventoryContainer.Amounts[selectedSlotItemIndex]);
            else
                selectedSlot.SetItem(selectedSlotItem);
        }

        Clear();
    }

    void Clear() {
        selectedSlot = null;
        selectedSlotIndex = -1;

        overSlot = null;
        overSlotIndex = -1;
    }

}