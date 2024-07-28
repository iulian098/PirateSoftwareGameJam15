using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] RecipesContainer recipesContainer;
    [SerializeField] UI_RecipeSlot recipePrefab;
    [SerializeField] Transform recipesParent;
    [SerializeField] TMP_InputField craftAmountInputField;
    [SerializeField] UI_MaterialSlot toCraftSlot;
    [SerializeField] UI_MaterialSlot[] materialsSlots;

    UI_RecipeSlot[] slots;

    RecipesContainer.Recipe selectedRecipe;
    int craftAmount;

    private void Start() {
        recipesContainer.Init();
        slots = new UI_RecipeSlot[recipesContainer.Recipes.Count];
        toCraftSlot.SetItem(null);
        foreach (var matSlot in materialsSlots) {
            matSlot.SetItem(null);
        }
        for (int i = 0; i < recipesContainer.Recipes.Count; i++) {
            slots[i] = Instantiate(recipePrefab, recipesParent);
            slots[i].Init(recipesContainer.Recipes[i], recipesContainer.Recipes[i].result, recipesContainer.Unlocked[i], OnRecipeSelected, CanCraft);
        }
    }

    void OnRecipeSelected(ItemData item) {
        RecipesContainer.Recipe recipe = recipesContainer.GetRecipe(item);
        int recipeIndex = recipesContainer.GetRecipeIndex(recipe);
        if (recipeIndex == -1 || !recipesContainer.Unlocked[recipeIndex]) return;
        toCraftSlot.SetItem(recipe.result);
        selectedRecipe = recipe;
        craftAmount = 1;
        craftAmountInputField.text = craftAmount.ToString();
        CanCraft();
    }

    void CanCraft() {
        for (int i = 0; i < materialsSlots.Length; i++) {
            if (i >= selectedRecipe.items.Length) {
                materialsSlots[i].SetItem(null);
                continue;
            }

            materialsSlots[i].SetItem(selectedRecipe.items[i], selectedRecipe.amounts[i] * craftAmount);

            int inventoryItemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedRecipe.items[i].ID);

            if (inventoryItemIndex != -1)
                materialsSlots[i].SetAvailable(inventoryContainer.Amounts[inventoryItemIndex] >= selectedRecipe.amounts[i] * craftAmount);
            else
                materialsSlots[i].SetAvailable(false);

        }
    }

    public bool CanCraft(RecipesContainer.Recipe recipe) {
        
        for (int i = 0; i < recipe.items.Length; i++) {
            int inventoryItemIndex = inventoryContainer.ItemsIDs.IndexOf(recipe.items[i].ID);

            if (inventoryItemIndex == -1)
                return false;

            if (inventoryContainer.Amounts[inventoryItemIndex] < recipe.amounts[i])
                return false;
        }

        return true;
    }

    public bool CanCraftAmount(RecipesContainer.Recipe recipe) {

        for (int i = 0; i < recipe.items.Length; i++) {
            int inventoryItemIndex = inventoryContainer.ItemsIDs.IndexOf(recipe.items[i].ID);

            if (inventoryItemIndex == -1)
                return false;

            if (inventoryContainer.Amounts[inventoryItemIndex] < selectedRecipe.amounts[i] * craftAmount)
                return false;
        }

        return true;
    }

    public void SetMaxAmount() {
        List<int> amounts = new List<int>();

        for (int i = 0; i < materialsSlots.Length; i++) {
            if (i >= selectedRecipe.items.Length)
                continue;


            int inventoryItemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedRecipe.items[i].ID);
            if (inventoryItemIndex == -1) continue;
            amounts.Add(inventoryContainer.Amounts[inventoryItemIndex] / selectedRecipe.amounts[i]);
        }

        craftAmount = amounts.Min();
        if (craftAmount <= 0)
            craftAmount = 1;
        craftAmountInputField.text = craftAmount.ToString();
        CanCraft();
    }

    public void OnCraftAmountChanged(string val) {
        if (selectedRecipe == null) return;

        int amount = int.Parse(val);
        if (amount <= 0) {
            amount = 1;
            craftAmountInputField.text = "1";
        }

        craftAmount = amount;
        CanCraft();
    }

    public void CraftItem() {
        if (CanCraftAmount(selectedRecipe)) {
            for (int i = 0; i < selectedRecipe.items.Length; i++) {
                int inventoryItemIndex = inventoryContainer.ItemsIDs.IndexOf(selectedRecipe.items[i].ID);
                inventoryContainer.SetAmountByIndex(inventoryItemIndex, inventoryContainer.Amounts[inventoryItemIndex] - (selectedRecipe.amounts[i] * craftAmount));
            }
            inventoryContainer.AddItem(selectedRecipe.result, craftAmount);
            SoundManager.Instance.PlaySound(transform.position, "Craft");
        }
        else {
            Debug.Log("Not enough resources");
        }
    }
}
