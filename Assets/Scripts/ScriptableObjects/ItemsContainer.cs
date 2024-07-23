using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsContainer", menuName = "Scriptable Objects/Items Container")]
public class ItemsContainer : ScriptableObject
{
    [SerializeField] ItemData[] items;

    bool initialized;
    Dictionary<int, ItemData> itemsDict = new Dictionary<int, ItemData>();

    public void Init() {
        foreach (var item in items) {
            if(!itemsDict.ContainsKey(item.ID))
                itemsDict.Add(item.ID, item);
        }
    }

    public ItemData GetItemByID(int ID) {
        if (!initialized) Init();

        if(itemsDict.TryGetValue(ID, out var item))
            return item;

        return null;
    }
}
