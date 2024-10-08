using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipesContainer", menuName = "Scriptable Objects/RecipesContainer")]
public class RecipesContainer : ScriptableObject
{
    [System.Serializable]
    public class Recipe {
        public ItemData result;
        public ItemData[] items;
        public int[] amounts;
    }

    [SerializeField] List<Recipe> recipes;
    [SerializeField] List<bool> unlocked;

    Dictionary<ItemData, Recipe> recipesDict = new Dictionary<ItemData, Recipe>();
    Dictionary<Recipe, int> recipesIndex = new Dictionary<Recipe, int>();

    public List<Recipe> Recipes => recipes;
    public List<bool> Unlocked => unlocked;

    public void Init() {
        /*for (int i = 0; i < recipes.Length; i++) {
            if (recipesDict.ContainsKey(recipes[i].result)) continue;
            recipesDict.Add(recipes[i].result, recipes[i]);
        }*/

        foreach (var recipe in recipes) {
            if (recipesDict.ContainsKey(recipe.result)) continue;
            recipesDict.Add(recipe.result, recipe);
        }
    }

    public Recipe GetRecipe(ItemData item) {
        if (recipesDict.ContainsKey(item)) 
            return recipesDict[item];
        return null;
    }

    public int GetRecipeIndex(Recipe recipe) {
        return recipes.IndexOf(recipe);
    }

    public void UnlockRecipe(ItemData item) {
        int index = recipes.FindIndex(x => x.result == item);
        if (index == -1) return;

        unlocked[index] = true;
    }

    public void SetSaveData(List<bool> unlockedData) {
        unlocked = unlockedData;

        if (unlocked.Count == 0) {
            for (int i = 0; i < recipes.Count; i++) {
                unlocked.Add(false);
            }
        }
    }
}
