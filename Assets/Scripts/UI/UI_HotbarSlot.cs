using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HotbarSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;
    [SerializeField] GameObject selectedObj;

    public Action<int> OnClickAction;
    public Action<int> OnSelected;

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

    public void SetSelected(bool selected) {
        selectedObj.SetActive(selected);
    }

    public override void OnPointerEnter(BaseEventData eventData) {
        HotbarManager.Instance.SetOverSlot(this);
    }

    public override void OnPointerExit(BaseEventData eventData) {
        HotbarManager.Instance.SetOverSlot(null);
    }

    public void OnClick() {
        Debug.Log($"Hotbar slot {slotIndex} clicked");
        if (item == null) return;
        OnClickAction?.Invoke(item.ID);
        OnSelected?.Invoke(slotIndex);
    }
}
