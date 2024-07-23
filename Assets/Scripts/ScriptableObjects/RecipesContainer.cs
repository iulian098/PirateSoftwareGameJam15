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

    [SerializeField] Recipe[] recipes;
    [SerializeField] bool[] unlocked;

    Dictionary<ItemData, Recipe> recipesDict = new Dictionary<ItemData, Recipe>();

    public Recipe[] Recipes => recipes;
    public bool[] Unlocked => unlocked;

    public void Init() {
        foreach (var recipe in recipes) {
            if (recipesDict.ContainsKey(recipe.result)) continue;
            recipesDict.Add(recipe.result, recipe);
        }
    }

}
