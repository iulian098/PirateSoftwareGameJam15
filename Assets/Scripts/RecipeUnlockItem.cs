using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUnlockItem : PickUp
{
    [SerializeField] ItemData itemRecipe;
    public override void OnPickup() {
        InGameManager.Instance.RecipesContainer.UnlockRecipe(itemRecipe);
        CommonPopup.Instance.Show($"You unlocked {itemRecipe.ItemName} recipe", null);
        Destroy(gameObject);
    }
}
