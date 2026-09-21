using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoSingleton<InventorySystem>
{
    public delegate void OnItemAdded(ItemData item, int itemIndex, int amount);
    public delegate void OnItemRemoved(ItemData item, int itemIndex);

    [SerializeField] HotbarManager hotbarManager;
    [SerializeField] ItemsContainer itemsContainer;
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] InventoryUI inventoryUI;

    public event OnItemAdded OnItemAddedCallback;
    InputAction inventoryAction;
    UI_Slot draggedSlot;
    int draggedSlotIndex;

    bool isDrag;
    public InventoryContainer InventoryContainer => inventoryContainer;
    public UI_Slot DraggedSlot => draggedSlot;
    public int DraggedSlotIndex => draggedSlotIndex;
    public bool IsDrag => isDrag;

    private void Start() {
        foreach (var item in inventoryContainer.InInventoryByDefault) {
            if (!inventoryContainer.ItemsIDs.Contains(item.ID))
                inventoryContainer.AddItem(item, 1);
        }

        inventoryAction = InGameManager.Instance.PlayerInput.actions["Inventory"];

    }

    private void Update()
    {
        if (inventoryAction.WasPerformedThisFrame())
        {
            if (UIManager.Instance.GetPanel<InventoryUI>(typeof(InventoryUI)) != null)
                UIManager.Instance.HidePanel<InventoryUI>(typeof(InventoryUI));
            else
                UIManager.Instance.ShowPanel(inventoryUI);
        }
    }

    public void Show() {
        inventoryUI.Show();
    }

    public void Hide() {
        UIManager.Instance.HidePanel<InventoryUI>(typeof(InventoryUI));
        UIManager.Instance.ItemInfo.Hide();
    }

    public bool AddItem(ItemData item, int amount) {
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
            UIManager.Instance.ShowPickupInfo(item, amount);

            OnItemAddedCallback?.Invoke(item, inventoryContainer.ItemsIDs[inventoryContainer.ItemsIDs.Count - 1], amount);
            return true;
        }

        if(inventoryContainer.ItemsIDs[itemIndex] == 0)
            inventoryContainer.ItemsIDs[itemIndex] = item.ID; 
        inventoryContainer.Amounts[itemIndex] += amount;

        OnItemAddedCallback?.Invoke(item, itemIndex, itemIndex);

        UIManager.Instance.ShowPickupInfo(item, amount);

        HotbarManager.Instance.AddItem(item);

        return true;

    }

    public void RemoveItem(ItemData item, int amount = 1) {
        inventoryContainer.RemoveItem(item.ID, amount);
    }

    public void SetDraggingSlot(UI_Slot slot)
    {
        draggedSlot = slot;
        draggedSlotIndex = slot != null ? slot.SlotIndex : -1;
    }

    public void OnEquipItem(int itemId) {
        EquipmentItemData item = itemsContainer.GetItemByID(itemId) as EquipmentItemData;
        InGameManager.Instance.Player.EquipWeapon(item.WeaponData, item);
    }
}
