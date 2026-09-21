using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : UIPanel
{
    [SerializeField] UI_Slot slotPrefab;
    [SerializeField] Transform slotsContainer;
    [SerializeField] ItemsContainer itemsContainer;
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] GameObject contents;

    [SerializeField] UI_Slot[] slots;

    [SerializeField] InventorySystem inventorySystem;

    UI_Slot selectedSlot;

    UI_Slot draggedSlot;
    int draggedSlotIndex;

    UI_Slot overSlot;
    int overSlotIndex;

    bool isDrag;
    ItemDragIcon ItemDragIcon => UIManager.Instance.ItemDragIcon;
    public InventoryContainer InventoryContainer => inventoryContainer;
    public UI_Slot DraggedSlot => draggedSlot;
    public int DraggedSlotIndex => draggedSlotIndex;
    public bool IsOpen => contents.activeSelf;
    public bool IsDrag => isDrag;

    private void Start()
    {
        InitSlots();
        inventoryContainer.OnInventoryUpdated += UpdateInventory;
    }

    private void OnDestroy()
    {
        inventoryContainer.OnInventoryUpdated -= UpdateInventory;
    }

    public override void Show()
    {
        if (!contents.activeSelf)
        {
            MouseHelper.Instance.OnDrag += HotbarManager.Instance.Drag;
            MouseHelper.Instance.OnDrag += Drag;
            MouseHelper.Instance.OnDrop += HotbarManager.Instance.Drop;
            MouseHelper.Instance.OnDrop += Drop;
        }

        GlobalData.isPaused = true;
        contents.SetActive(true);
        UIHandCursor.Instance.Show();

        inventorySystem.OnItemAddedCallback += OnItemAdded;

        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Gamepad)
            EventSystem.current.SetSelectedGameObject(slots[0].gameObject);
    }

    public override void Hide()
    {
        if (contents.activeSelf)
        {
            MouseHelper.Instance.OnDrag -= HotbarManager.Instance.Drag;
            MouseHelper.Instance.OnDrag -= Drag;
            MouseHelper.Instance.OnDrag -= HotbarManager.Instance.Drop;
            MouseHelper.Instance.OnDrop -= Drop;
        }

        GlobalData.isPaused = false;
        UIHandCursor.Instance.Hide();
        ItemDragIcon.Hide();
        UIManager.Instance.ItemInfo.Hide();
        contents.SetActive(false);
        Cursor.visible = true;
        selectedSlot = null;
    }

    private void Update()
    {
        if (IsOpen)
        {
            if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.KeyboardAndMouse)
            {
                if (isDrag)
                    ItemDragIcon.UpdatePosition(MouseHelper.Instance.MousePos);

                UIHandCursor.Instance.SetPosition(MouseHelper.Instance.MousePos);
            }
            else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Gamepad)
            {
                if (!isDrag && InGameManager.Instance.PlayerInput.actions["ConsumableSlot"].WasPressedThisFrame())
                    Drag(selectedSlot);

                if (isDrag && InGameManager.Instance.PlayerInput.actions["Use"].WasPressedThisFrame())
                    Drop();
            }


        }
    }

    void InitSlots()
    {
        slots = new UI_Slot[inventoryContainer.ItemsIDs.Count];
        for (int i = 0; i < inventoryContainer.ItemsIDs.Count; i++)
        {
            slots[i] = Instantiate(slotPrefab, slotsContainer);
#if UNITY_EDITOR
            slots[i].name = "Slot" + i;
#endif
            int tmp = i;
            slots[i].SetSlotIndex(tmp);
            slots[i].SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[i]), inventoryContainer.Amounts[i]);
            slots[i].OnSlotSelected += OnSlotSelected;
            slots[i].OnSlotPointerEnter += OnSlotPointerEnter;
            slots[i].OnSlotPointerExit += OnSlotPointerExit;
        }
    }

    private void OnSlotPointerEnter(UI_Slot slot)
    {
        overSlot = slot;
        overSlotIndex = slot == null ? -1 : slot.SlotIndex;
    }

    private void OnSlotPointerExit(UI_Slot slot)
    {
        overSlot = null;
        overSlotIndex = -1;
    }

    private void OnItemAdded(ItemData item, int itemIndex, int amount)
    {
        slots[itemIndex].UpdateItem(inventoryContainer, itemsContainer);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetItem(itemsContainer.GetItemByID(inventoryContainer.ItemsIDs[i]), inventoryContainer.Amounts[i]);
    }

    private void OnSlotSelected(UI_Slot slot)
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.KeyboardAndMouse)
            return;

        selectedSlot = slot;
        overSlot = slot;
        overSlotIndex = slot.SlotIndex;

        if (isDrag)
            ItemDragIcon.UpdatePosition(overSlot.transform.position + new Vector3(1, 1, 0));
    }

    public void SetOverSlot(UI_Slot slot)
    {
        overSlot = slot;
        overSlotIndex = slot == null ? -1 : slot.SlotIndex;
    }

    public void Drag()
    {
        UIManager.Instance.ItemInfo.Hide();
        if (overSlot == null) 
            return;

        isDrag = true;
        draggedSlot = overSlot;
        draggedSlotIndex = overSlot.SlotIndex;

        if (draggedSlot.Item == null) 
            return;

        ItemDragIcon.Show(overSlot.Item.Icon, MouseHelper.Instance.MousePos);

        UIHandCursor.Instance.SetHandSprite(UIHandCursor.SpriteType.Drag);
        UIHandCursor.Instance.SetPosition(draggedSlot.transform.position);

        inventorySystem.SetDraggingSlot(draggedSlot);

        HotbarManager.Instance.SetDisabled(overSlot.Item.Type != Enum_ItemType.Equipment);
        HotbarManager.Instance.SetConsumableDisabled(overSlot.Item.Type != Enum_ItemType.Consumable);
        SoundManager.Instance.PlaySound(transform.position, "ItemDrag");
    }

    public void Drag(UI_Slot slot)
    {
        UIManager.Instance.ItemInfo.Hide();

        if (overSlot == null) 
            return;

        isDrag = true;
        draggedSlot = overSlot;
        draggedSlotIndex = overSlot.SlotIndex;

        if (draggedSlot.Item == null) 
            return;

        ItemDragIcon.Show(overSlot.Item.Icon, overSlot.transform.position + new Vector3(1, 1, 0));
        inventorySystem.SetDraggingSlot(draggedSlot);

        HotbarManager.Instance.SetDisabled(overSlot.Item.Type != Enum_ItemType.Equipment);
        HotbarManager.Instance.SetConsumableDisabled(overSlot.Item.Type != Enum_ItemType.Consumable);
        SoundManager.Instance.PlaySound(transform.position, "ItemDrag");
    }

    public void Drop()
    {
        isDrag = false;
        ItemDragIcon.Hide();
        HotbarManager.Instance.SetDisabled(false);
        HotbarManager.Instance.SetConsumableDisabled(false);

        UIHandCursor.Instance.SetHandSprite(UIHandCursor.SpriteType.Normal);

        if (draggedSlot == null || overSlot == null || draggedSlot == overSlot)
        {
            Clear();
            return;
        }

        inventoryContainer.SwapItems(overSlotIndex, draggedSlotIndex);

        draggedSlot.UpdateItem(inventoryContainer, itemsContainer);
        overSlot.UpdateItem(inventoryContainer, itemsContainer);

        if (overSlot.Item != null)
            UIManager.Instance.ItemInfo.Show(overSlot.Item, overSlot.transform.position - new Vector3(0, (overSlot.transform as RectTransform).sizeDelta.y / 2, 0));

        SoundManager.Instance.PlaySound(transform.position, "ItemDrop");


        Clear();
    }

    void Clear()
    {
        draggedSlot = null;
        draggedSlotIndex = -1;
        inventorySystem.SetDraggingSlot(null);
    }
}
