using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryContainer", menuName = "Scriptable Objects/Inventory Container")]
public class InventoryContainer : ScriptableObject
{
    [SerializeField] List<int> itemsIDs = new List<int>();
    [SerializeField] List<int> itemsAmounts = new List<int>();
    [SerializeField] List<int> hotbarIDs = new List<int>();
    [SerializeField] int consumableID;
    [SerializeField] int hotbarSelectedIndex;
    [SerializeField] List<ItemData> inInventoryByDefualt = new List<ItemData>();

    public List<int> ItemsIDs => itemsIDs;
    public List<int> Amounts => itemsAmounts;
    public List<int> HotbarIDs => hotbarIDs;
    public int HotbarSelectedIndex { get => hotbarSelectedIndex; set => hotbarSelectedIndex = value; }
    public int ConsumableID { get => consumableID; set => consumableID = value; }
    public List<ItemData> InInventoryByDefault => inInventoryByDefualt;

    public Action OnInventoryUpdated;

    public int AddItem(ItemData item, int amount) {
        return AddItem(item.ID, amount);
    }

    public int AddItem(int itemID, int amount) {
        int slotIndex = -1;

        int itemIndex = itemsIDs.IndexOf(itemID);
        if(itemIndex == -1) { 
            for (int i = 0; i < itemsIDs.Count; i++) {
                if (itemsIDs[i] <= 0) {
                    slotIndex = i; 
                    break;
                }
            }

            if (slotIndex == -1) return -1;
        }
        else {
            slotIndex = itemIndex;
        }
        itemsIDs[slotIndex] = itemID;
        itemsAmounts[slotIndex] += amount;

        OnInventoryUpdated?.Invoke();
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

    public void SetAmountByID(int itemID, int amount) {
        int itemIndex = itemsIDs.IndexOf(itemID);
        if (itemIndex == -1) return;

        itemsAmounts[itemIndex] = amount;

        if (itemsAmounts[itemIndex] == 0) {
            itemsIDs[itemIndex] = 0;
        }

        OnInventoryUpdated?.Invoke();
    }

    public void RemoveItem(int itemID, int amount = 1) {
        int itemIndex = itemsIDs.IndexOf(itemID);
        if (itemIndex == -1) return;

        itemsAmounts[itemIndex] -= amount;

        if (itemsAmounts[itemIndex] == 0)
            itemsIDs[itemIndex] = 0;

        OnInventoryUpdated?.Invoke();
    }

    public void SetAmountByIndex(int itemIndex, int amount) {
        if (itemIndex == -1) return;

        itemsAmounts[itemIndex] = amount;

        if (itemsAmounts[itemIndex] == 0) {
            itemsIDs[itemIndex] = 0;
        }

        OnInventoryUpdated?.Invoke();
    }

    public void SetSaveData(List<int> itemsIDs, List<int> itemsAmounts, List<int> hotbarIDs, int hotbarSelectedIndex) {
        this.itemsIDs = itemsIDs;
        this.itemsAmounts = itemsAmounts;
        this.hotbarIDs = hotbarIDs;
        this.hotbarSelectedIndex = hotbarSelectedIndex;

        if(itemsIDs.Count == 0 || itemsAmounts.Count == 0) {
            for(int i = 0; i < 10; i++) {
                this.itemsIDs.Add(0);
                this.itemsAmounts.Add(0);
            }
        }

        if(hotbarIDs.Count == 0) {
            for (int i = 0; i < 5; i++) {
                this.hotbarIDs.Add(0);
            }


            for (int i = 0; i < inInventoryByDefualt.Count; i++) {
                AddItem(inInventoryByDefualt[i], 1);
            }
        }
    }
}
