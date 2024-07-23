using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryContainer", menuName = "Scriptable Objects/Inventory Container")]
public class InventoryContainer : ScriptableObject
{
    [SerializeField] List<int> itemsIDs = new List<int>();
    [SerializeField] List<int> itemsAmounts = new List<int>();

    public List<int> ItemsIDs => itemsIDs;
    public List<int> Amounts => itemsAmounts;

    public int AddItem(ItemData item, int amount) {
        return AddItem(item.ID, amount);
    }

    public int AddItem(int itemID, int amount) {
        int slotIndex = -1;

        for (int i = 0; i < itemsIDs.Count; i++) {
            if (itemsIDs[i] == -1) {
                slotIndex = i; 
                break;
            }
        }

        if (slotIndex == -1) return -1;

        itemsIDs[slotIndex] = itemID;
        itemsAmounts[slotIndex] = amount;

        return slotIndex;
    }

    public void SwapItems(int index1, int index2) {

        int auxID = itemsIDs[index1];
        itemsIDs[index1] = itemsIDs[index2];
        itemsIDs[index2] = auxID;

        int auxAmount = itemsAmounts[index1];
        itemsAmounts[index1] = itemsAmounts[index2];
        itemsAmounts[index2] = auxAmount;

    }
}
