using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : MonoBehaviour
{
    [SerializeField] protected Image iconImage;

    protected int slotIndex;
    protected ItemData item;

    public int SlotIndex => slotIndex;
    public ItemData Item => item;

    public virtual void SetItem(ItemData item) {
        this.item = item;
    }

    public void SetSlotIndex (int index) {
        Debug.Log("Set slot index " + index);
        slotIndex = index;
    }

    public virtual void UpdateUI() {}

    public virtual void OnDrag(BaseEventData eventData) { }
    public virtual void OnEndDrag(BaseEventData eventData){ }
    public virtual void OnDrop(BaseEventData eventData){ }
    public virtual void OnPointerEnter(BaseEventData eventData){ }
    public virtual void OnPointerExit(BaseEventData eventData){ }
}
