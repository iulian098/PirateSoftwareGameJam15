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

    public override void SetItem(ItemData item, int amount) {
        base.SetItem(item, amount);
        UpdateUI();
    }

    public override void UpdateUI() {
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
        //InventorySystem.Instance.Drag(this);
    }

    public override void OnDrop(BaseEventData eventData) {
        //InventorySystem.Instance.Drop();
        Debug.Log($"{name} Drop");
    }

    public override void OnPointerEnter(BaseEventData eventData) {
        InventorySystem.Instance.SetOverSlot(this);
        if(Item != null && !InventorySystem.Instance.IsDrag)
            UIManager.Instance.ItemInfo.Show(Item, transform.position - new Vector3(0, (transform as RectTransform).sizeDelta.y / 2, 0));
    }

    public override void OnPointerExit(BaseEventData eventData) {

        InventorySystem.Instance.SetOverSlot(null);
        UIManager.Instance.ItemInfo.Hide();
    }
}
