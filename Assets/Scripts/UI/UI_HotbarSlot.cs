using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HotbarSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;
    [SerializeField] GameObject selectedObj;
    [SerializeField] GameObject disabledObj;
    [SerializeReference] KeyIcon[] keybindIcons;
    [SerializeField] GameObject keybindContainer;

    public Action<int> OnClickAction;
    public Action<int> OnClicked;

    public KeyIcon[] KeybindIcons => keybindIcons;
    public GameObject KeybindContainer => keybindContainer;

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
            if(item is EquipmentItemData && (item as EquipmentItemData).IsInfinite)
                amountText.gameObject.SetActive(false);
            else
                amountText.gameObject.SetActive(true);
        }

        iconImage.sprite = item.Icon;
        amountText.text = $"x{amount}";
    }

    public void UpdateKeybind(InputDeviceType type)
    {
        foreach (var keybind in keybindIcons)
        {
            keybind.DeviceChanged(type);
        }
    }

    public void SetSelected(bool selected) {
        selectedObj.SetActive(selected);
    }

    public void SetDisabled(bool disabled) {
        disabledObj.SetActive(disabled);
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        HotbarManager.Instance.SetOverSlot(this);
    }

    public override void OnPointerExit(PointerEventData eventData) {
        HotbarManager.Instance.SetOverSlot(null);
    }

    public void OnClick() {
        Debug.Log($"Hotbar slot {slotIndex} clicked");
        if (item == null) return;
        OnClickAction?.Invoke(item.ID);
        OnClicked?.Invoke(slotIndex);
    }

    public override void Clear() {
        OnClickAction = null;
        OnClicked = null;
    }
}
