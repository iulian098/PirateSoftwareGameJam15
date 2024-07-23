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
    RecipesContainer.Recipe recipe;
    ItemData item;
    Func<RecipesContainer.Recipe, bool> CanCraft;

    public void Init(RecipesContainer.Recipe recipe, ItemData itemData, bool isUnlocked, Action<ItemData> onSelected, Func<RecipesContainer.Recipe, bool> canCraft) {
        item = itemData;
        icon.sprite = itemData.Icon;
        this.recipe = recipe;
        CanCraft = canCraft;
        if (isUnlocked) {
            if(canCraft.Invoke(this.recipe))
                icon.color = canCraftColor;
            else
                icon.color = noResourcesColor;
        }
        else
            icon.color = lockedColor;

        OnSelected = onSelected;
    }

    public void UpdateUI() {
        if (CanCraft.Invoke(recipe))
            icon.color = canCraftColor;
        else
            icon.color = noResourcesColor;
    }

    public void OnClick() {
        OnSelected?.Invoke(item);
    }
}
