using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PotionSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;

    public override void SetItem(ItemData item) {
        base.SetItem(item);

        UpdateUI();
    }

    public override void UpdateUI() {
        if (item == null) {
            iconImage.gameObject.SetActive(false);
            return;
        }
        else
            iconImage.gameObject.SetActive(true);

        iconImage.sprite = item.Icon;
    }

    public override void OnDrag(BaseEventData eventData) {
        InventorySystem.Instance.Drag(this);
    }

    public override void OnDrop(BaseEventData eventData) {
        InventorySystem.Instance.Drop();
    }

    public override void OnPointerEnter(BaseEventData eventData) {
        InventorySystem.Instance.SetOverSlot(this);
    }
}
