using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] RecipesContainer recipesContainer;
    [SerializeField] UI_RecipeSlot recipePrefab;
    [SerializeField] Transform recipesParent;

    UI_RecipeSlot[] slots;

    private void Start() {
        recipesContainer.Init();
        slots = new UI_RecipeSlot[recipesContainer.Recipes.Length];

        for (int i = 0; i < recipesContainer.Recipes.Length; i++) {
            slots[i] = Instantiate(recipePrefab, recipesParent);
            slots[i].Init(recipesContainer.Recipes[i].result, recipesContainer.Unlocked[i]);
        }
    }
}
