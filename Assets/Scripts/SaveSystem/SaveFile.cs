using System;
using System.Collections.Generic;

public class SaveFile
{
    public DateTime saveDate;
    public PlayerData playerData;
    public List<int> inventoryItemsIds;
    public List<int> inventoryItemsAmounts;
    public List<int> hotbarItems;
    public List<bool> unlockedRecipes;
    public int hotbarSelectedSlot;

    public SaveFile() {
        playerData = new PlayerData();
        inventoryItemsIds = new List<int>();
        inventoryItemsAmounts = new List<int>();
        hotbarItems = new List<int>();
        unlockedRecipes = new List<bool>();
        hotbarSelectedSlot = 0;
    }
}
