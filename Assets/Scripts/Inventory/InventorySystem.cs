using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoSingleton<InventorySystem>
{
    [SerializeField] UI_Slot[] slots;
    [SerializeField] ItemsContainer itemsContainer;
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] ItemData testItemData;
    [SerializeField] GameObject contents;
    [SerializeField] Transform handTransform;
    [SerializeField] Image handImage;
    [SerializeField] Sprite[] handSprites;

    public Action<ItemData> OnItemAdded;

    UI_Slot selectedSlot;
    int selectedSlotIndex;

    UI_Slot overSlot;
    int overSlotIndex;

    bool isDrag;

    ItemDragIcon ItemDragIcon => UIManager.Instance.ItemDragIcon;
    public UI_Slot SelectedSlot => selectedSlot;
    public int SelectedSlotIndex => selectedSlotIndex;
    public bool IsOpen => contents.activeSelf;
    public bool IsDrag => isDrag;

    private void Start() {
        //items = new ItemData[slots.Length];
        /*items[0] = testItemData;
        items[slots.Length - 1] = testItemData;*/
        inventoryContainer.OnInventoryUpdated += UpdateInventory;
        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            slots[i].SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[i]), inventoryContainer.Amounts[i]);
        }
    }

    private void OnDestroy() {
        inventoryContainer.OnInventoryUpdated -= UpdateInventory;
    }

    private void Update() {
        if (IsOpen) {
            handTransform.position = InGameManager.Instance.PlayerInput.actions["AimDirection"].ReadValue<Vector2>();
            if (isDrag && handImage.sprite != handSprites[1])
                handImage.sprite = handSprites[1];
            else if (!isDrag && handImage.sprite != handSprites[0])
                handImage.sprite = handSprites[0];
        }

    }

    private void FixedUpdate() {
        if (isDrag)
            ItemDragIcon.transform.position = InGameManager.Instance.PlayerInput.actions["AimDirection"].ReadValue<Vector2>();

    }

    public void Show() {
        contents.SetActive(true);
        Cursor.visible = false;
    }

    public void Hide() {
        contents.SetActive(false);
        Cursor.visible = true;
    }

    public bool AddItem(ItemData item, int amount) {
        /*int result = inventoryContainer.AddItem(item, amount);

        if (result != -1) {
            OnItemAdded?.Invoke(item);
            slots[result].SetItem(item, amount);
        }

        return result != -1 ? true : false;*/

        //Auto Combine
        /*for(int i = 0; i < inventoryContainer.ItemsIDs.Count; i++) {
            if(inventoryContainer.ItemsIDs[i] == item.ID && item.MaxStack > 1 && inventoryContainer.Amounts[i] < item.MaxStack) {
                if (inventoryContainer.Amounts[i] + amount <= item.MaxStack) {
                    inventoryContainer.Amounts[i] += amount;
                }
                else {
                    int toAdd = item.MaxStack - amount;
                    inventoryContainer.Amounts[i] += toAdd;
                   // amount -= toAdd;
                    //AddItem(item, amount);
                }
            }
            else if (inventoryContainer.ItemsIDs[i] == 0){
                inventoryContainer.ItemsIDs[i] = item.ID;
                if (amount <= item.MaxStack)
                    inventoryContainer.Amounts[i] = amount;
                else {
                    inventoryContainer.Amounts[i] = item.MaxStack;
                    //amount -= item.MaxStack;
                    //AddItem(item, amount);
                }

                return true;
            }
        }*/

        int itemIndex = -1;
        for (int i = 0; i < inventoryContainer.ItemsIDs.Count; i++) {
            if (inventoryContainer.ItemsIDs[i] == item.ID) {
                itemIndex = i;
                break;
            }
        }

        //No item found, find an empty slot
        if(itemIndex == -1) {
            for (int i = 0; i < inventoryContainer.ItemsIDs.Count; i++) {
                if (inventoryContainer.ItemsIDs[i] == 0) {
                    itemIndex = i;
                    break;
                }
            }
        }

        //Adding new slot
        if (itemIndex == -1) {
            inventoryContainer.ItemsIDs.Add(item.ID);
            inventoryContainer.Amounts.Add(amount);

            return true;
        }

        if(inventoryContainer.ItemsIDs[itemIndex] == 0)
            inventoryContainer.ItemsIDs[itemIndex] = item.ID; 
        inventoryContainer.Amounts[itemIndex] += amount;
        slots[itemIndex].UpdateItem(inventoryContainer, itemsContainer);
        return true;

    }

    public void SetOverSlot(UI_Slot slot) {
        overSlot = slot;
        overSlotIndex = slot == null ? -1 : slot.SlotIndex;
    }

    public void Drag(UI_Slot slot) {
        UIManager.Instance.ItemInfo.Hide();
        isDrag = true;
        selectedSlot = slot;
        selectedSlotIndex = slot.SlotIndex;
        if (selectedSlot.Item == null) return;
        ItemDragIcon.Show(slot.Item.Icon);
        HotbarManager.Instance.SetDisabled(slot.Item.Type != Enum_ItemType.Equipment);
    }

    public void Drop() {
        isDrag = false;
        ItemDragIcon.Hide();
        HotbarManager.Instance.SetDisabled(false);
        if (selectedSlot == null || overSlot == null || selectedSlot == overSlot) {
            Clear();
            return;
        }

        //Combine items
        /*if(overSlot.Item == selectedSlot.Item) {
            if (inventoryContainer.Amounts[overSlotIndex] < overSlot.Item.MaxStack) {
                if (inventoryContainer.Amounts[overSlotIndex] + inventoryContainer.Amounts[selectedSlotIndex] < overSlot.Item.MaxStack) {
                    inventoryContainer.Amounts[overSlotIndex] += inventoryContainer.Amounts[selectedSlotIndex];
                    inventoryContainer.Amounts[selectedSlotIndex] = 0;
                    inventoryContainer.ItemsIDs[selectedSlotIndex] = 0;
                }
                else {
                    int toAdd = overSlot.Item.MaxStack - inventoryContainer.Amounts[overSlotIndex];
                    inventoryContainer.Amounts[overSlotIndex] += toAdd;
                    inventoryContainer.Amounts[selectedSlotIndex] -= toAdd;
                }
            }

            selectedSlot.UpdateItem(inventoryContainer, itemsContainer);
            overSlot.UpdateItem(inventoryContainer, itemsContainer);
            return;
        }*/

        inventoryContainer.SwapItems(overSlotIndex, selectedSlotIndex);

        selectedSlot.UpdateItem(inventoryContainer, itemsContainer);
        overSlot.UpdateItem(inventoryContainer, itemsContainer);
        /*overSlot.SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[overSlotIndex]), inventoryContainer.Amounts[overSlotIndex]);
        selectedSlot.SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[selectedSlotIndex]), inventoryContainer.Amounts[selectedSlotIndex]);*/

        /*ItemData aux = items[overSlotIndex];

        items[overSlotIndex] = items[selectedSlotIndex];
        items[selectedSlotIndex] = aux;

        overSlot.SetItem(items[overSlotIndex]);
        selectedSlot.SetItem(items[selectedSlotIndex]);

        Debug.Log($"[Inventory] On Item Dropped {selectedSlot.name} - {overSlot.name}");*/
        if(overSlot.Item != null)
            UIManager.Instance.ItemInfo.Show(overSlot.Item, overSlot.transform.position - new Vector3(0, (overSlot.transform as RectTransform).sizeDelta.y / 2, 0));
        Clear();
    }

    public void UpdateInventory() {
        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            slots[i].SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[i]), inventoryContainer.Amounts[i]);
        }
    }

    void Clear() {
        selectedSlot = null;
        selectedSlotIndex = -1;
    }

}
