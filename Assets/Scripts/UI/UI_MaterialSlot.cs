using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MaterialSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color fadeColor = Color.gray;

    public override void SetItem(ItemData item) {
        base.SetItem(item);
        if (item != null) {
            iconImage.sprite = item.Icon;
            iconImage.gameObject.SetActive(true);
        }
        else
            iconImage.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
    }

    public override void SetItem(ItemData item, int amount) {
        base.SetItem(item, amount);
        if (item != null) {
            iconImage.sprite = item.Icon;
            iconImage.gameObject.SetActive(true);
            amountText.gameObject.SetActive(true);
        }
        else {
            iconImage.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
        }
        amountText.text = amount.ToString();
    }

    public void SetAvailable(bool available) {
        iconImage.color = available ? normalColor : fadeColor;
    }


}
