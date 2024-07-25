using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemPickupInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text amountText;

    public void Init(ItemData item, int amount) {
        icon.sprite = item.Icon;
        amountText.text = $"+{amount}";
    }
}
