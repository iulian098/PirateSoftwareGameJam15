using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoSingleton<HotbarManager>
{
    [SerializeField] UI_HotbarSlot[] slots;
    [SerializeField] UI_HotbarSlot consumableSlot;
    [SerializeField] ItemsContainer itemsContainer;
    [SerializeField] InventoryContainer inventoryContainer;

    UI_Slot selectedSlot;
    int selectedSlotIndex = -1;

    UI_Slot overSlot;
    int overSlotIndex = -1;

    int activeSlot = -1;
    ItemDragIcon ItemDragIcon => UIManager.Instance.ItemDragIcon;

    InputAction[] hotbarActions;
    InputAction hotbarConsumable;

    private void Start() {
        hotbarActions = new InputAction[slots.Length];
        for (int i = 0; i < slots.Length; i++) {
            int tmp = i;
            slots[i].SetSlotIndex(tmp);

            EquipmentItemData item = itemsContainer.GetItemByID(inventoryContainer.HotbarIDs[i]) as EquipmentItemData;
            if (item != null) {
                int amountIndex = inventoryContainer.ItemsIDs.IndexOf(item.ID);
                if (amountIndex == -1)
                    slots[i].SetItem(null);
                else
                    slots[i].SetItem(item, inventoryContainer.Amounts[amountIndex]);
            }
            else
                slots[i].SetItem(null);
            slots[i].OnClickAction += InventorySystem.Instance.OnEquipItem;
            slots[i].OnSelected += OnSlotClicked;

            hotbarActions[i] = InGameManager.Instance.PlayerInput.actions["Hotbar" + (i + 1)];
        }
        if(inventoryContainer.ConsumableID == 0)
            consumableSlot.SetItem(null);
        else {
            ConsumableItemData item = itemsContainer.GetItemByID(inventoryContainer.ConsumableID) as ConsumableItemData;
            if(item != null) {
                int amountIndex = inventoryContainer.ItemsIDs.IndexOf(item.ID);
                if(amountIndex != -1)
                    consumableSlot.SetItem(item, inventoryContainer.Amounts[amountIndex]);
                else
                    consumableSlot.SetItem(null);
            }
        }
        activeSlot = inventoryContainer.HotbarSelectedIndex;
        OnSlotClicked(activeSlot);
        hotbarConsumable = InGameManager.Instance.PlayerInput.actions["ConsumableSlot"];
        consumableSlot.OnClickAction += UseConsumable;
        inventoryContainer.OnInventoryUpdated += UpdateUI;
        slots[inventoryContainer.HotbarSelectedIndex].OnClick();
    }

    private void OnDestroy() {
        inventoryContainer.OnInventoryUpdated -= UpdateUI;
        //consumableSlot.OnClickAction -= UseConsumable;
        consumableSlot.Clear();

        foreach (var slot in slots) {
            slot.Clear();
        }
    }

    private void OnSlotClicked(int index) {
        if (GlobalData.isPaused) return;
        if (activeSlot != -1)
            slots[activeSlot].SetSelected(false);
        activeSlot = index;
        slots[activeSlot].SetSelected(true);
        inventoryContainer.HotbarSelectedIndex = index;
    }

    private void Update() {
        if (GlobalData.isPaused) return;
        for (int i = 0; i < slots.Length; i++) {
            if (hotbarActions[i].WasPerformedThisFrame()) { 
                slots[i].OnClick();
                if(activeSlot != -1)
                    slots[activeSlot].SetSelected(false);
                activeSlot = i;
                slots[activeSlot].SetSelected(true);
            }
        }
        if (hotbarConsumable.WasPerformedThisFrame())
            consumableSlot.OnClick();
    }

    public void UpdateUI() {
        
        foreach (var slot in slots) {
            /*if (slot.Item == null) continue;

            int itemIndex = inventoryContainer.ItemsIDs.IndexOf(slot.Item.ID);
            if (itemIndex == -1) {
                slot.SetItem(null);
                continue;
            }
            int amount = inventoryContainer.Amounts[itemIndex];

            if (amount == 0)
                slot.SetItem(null);
            else
                slot.SetItem(slot.Item, amount);

            slot.UpdateUI();*/
            UpdateSlot(slot);
        }

        UpdateSlot(consumableSlot);
    }

    public void UpdateSlot(UI_HotbarSlot slot) {
        if (slot.Item == null) return;

        int itemIndex = inventoryContainer.ItemsIDs.IndexOf(slot.Item.ID);
        if (itemIndex == -1) {
            slot.SetItem(null);
            return;
        }
        int amount = inventoryContainer.Amounts[itemIndex];

        if (amount == 0)
            slot.SetItem(null);
        else
            slot.SetItem(slot.Item, amount);

        slot.UpdateUI();

    }

    public void SetDisabled(bool disable) {
        //disabledObj.SetActive(disable);
        foreach (var slot in slots) {
            slot.SetDisabled(disable);
        }
    }

    public void SetConsumableDisabled(bool disable) {
        consumableSlot.SetDisabled(disable);
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

    public void Drag() {
        if (overSlot == null) return;

        selectedSlot = overSlot;
        selectedSlotIndex = overSlot.SlotIndex;

        if (selectedSlot.Item == null) return;
        ItemDragIcon.Show(selectedSlot.Item.Icon);
    }

    public void Drop() {
        ItemDragIcon.Hide();
        if (selectedSlot == null && overSlot != null) {
            selectedSlot = InventorySystem.Instance.SelectedSlot;
            selectedSlotIndex = InventorySystem.Instance.SelectedSlotIndex;
            if (selectedSlot != null && selectedSlot.Item != null && overSlot != null) {
                if (selectedSlot.Item.Type == Enum_ItemType.Equipment) {
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
                    if (overSlot.SlotIndex == activeSlot) {
                        InventorySystem.Instance.OnEquipItem(overSlot.Item.ID);
                    }

                    Clear();
                }else if(selectedSlot.Item.Type == Enum_ItemType.Consumable) {
                    if (overSlot == consumableSlot) {
                        int itemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedSlot.Item.ID);
                        if (itemIndex != -1) {
                            int itemAmount = inventoryContainer.Amounts[itemIndex];
                            overSlot.SetItem(selectedSlot.Item, itemAmount);
                        }else
                            overSlot.SetItem(selectedSlot.Item);
                        inventoryContainer.ConsumableID = selectedSlot.Item.ID;
                    }
                    Clear();
                }

            }


            return;
        }else if(selectedSlot != null && overSlot == null && selectedSlot is UI_HotbarSlot) {
            selectedSlot.SetItem(null);
            inventoryContainer.HotbarIDs[selectedSlotIndex] = 0;
            Clear();
        }

        if (selectedSlot == null || overSlot == null || overSlot == selectedSlot || overSlot == consumableSlot) return;

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

    public void UseConsumable(int itemID) {
        if (consumableSlot.Item == null) return;
        ConsumableItemData consumable = consumableSlot.Item as ConsumableItemData;

        switch (consumable.ModifyStat) {
            case Enum_ModifyStat.Health:
                InGameManager.Instance.Player.HealthComponent.Health += (int)consumable.Amount;
                Instantiate(consumable.OnConsumeVFX, InGameManager.Instance.Player.transform);
                SoundManager.Instance.PlaySound(InGameManager.Instance.Player.transform.position, "PotionOpen");
                break;
            case Enum_ModifyStat.Speed:
                break;
            case Enum_ModifyStat.Defense:
                break;
            default:
                break;
        }

        inventoryContainer.RemoveItem(consumable.ID);
    }

    void Clear() {
        selectedSlot = null;
        selectedSlotIndex = -1;

        overSlot = null;
        overSlotIndex = -1;
    }

}
