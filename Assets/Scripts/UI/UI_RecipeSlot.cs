using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RecipeSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Color canCraftColor;
    [SerializeField] Color noResourcesColor;
    [SerializeField] Color lockedColor;

    public Action<ItemData> OnSelected;
    ItemData item;

    public void Init(ItemData itemData, bool isUnlocked) {
        item = itemData;
        icon.sprite = itemData.Icon;
        if (isUnlocked) {
            icon.color = canCraftColor;
        }
        else {
            icon.color = lockedColor;
        }
    }

    public void OnClick() {
        OnSelected?.Invoke(item);
    }
}
