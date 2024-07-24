using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HotbarSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;

    public override void SetItem(ItemData item) {
        base.SetItem(item);
        UpdateUI();
    }

    public override void SetItem(ItemData item, int amount) {
        base.SetItem(item, amount);
        UpdateUI();
    }

    public override void UpdateUI() {
        base.UpdateUI();

        if (item == null) {
            iconImage.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
            return;
        }
        else {
            iconImage.gameObject.SetActive(true);
            amountText.gameObject.SetActive(true);
        }

        iconImage.sprite = item.Icon;
        amountText.text = $"x{amount}";
    }

    public override void OnDrag(BaseEventData eventData) {
        HotbarManager.Instance.Drag(this);
    }

    public override void OnDrop(BaseEventData eventData) {
        HotbarManager.Instance.Drop();
    }

    public override void OnPointerEnter(BaseEventData eventData) {
        HotbarManager.Instance.SetOverSlot(this);
    }

    public override void OnPointerExit(BaseEventData eventData) {
        HotbarManager.Instance.SetOverSlot(null);
    }
}
