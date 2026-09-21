using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler, ISelectHandler
{
    [SerializeField] protected Image iconImage;

    public event Action<UI_Slot> OnSlotSelected;
    public event Action<UI_Slot> OnSlotPointerEnter;
    public event Action<UI_Slot> OnSlotPointerExit;
    public event Action<UI_Slot> OnSlotDrag;
    public event Action<UI_Slot> OnSlotDrop;

    protected int slotIndex;
    protected ItemData item;
    protected int amount;

    public int SlotIndex => slotIndex;
    public ItemData Item => item;
    public int Amount => amount;

    public virtual void SetItem(ItemData item) {
        this.item = item;
    }

    public virtual void SetItem(ItemData item, int amount) {  
        this.item = item;
        this.amount = amount;
    }

    public void SetSlotIndex (int index) {
        slotIndex = index;
    }

    public virtual void UpdateItem(InventoryContainer inventory, ItemsContainer items) {
        ItemData item =  items.GetItemByID(inventory.ItemsIDs[slotIndex]);
        int amount = inventory.Amounts[slotIndex];
        SetItem(item, amount);
    }

    public virtual void UpdateUI() {}

    public virtual void Clear() { }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnSlotPointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnSlotPointerExit?.Invoke(this);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        OnSlotDrag?.Invoke(this);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        OnSlotDrop?.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnSlotSelected?.Invoke(this);
    }
}
