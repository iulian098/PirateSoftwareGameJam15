using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickupInfo : MonoBehaviour
{
    [SerializeField] ItemPickupEntry itemPickupPrefab;
    [SerializeField] Transform entriesParent;

    PoolingSystem<ItemPickupEntry> entriesPooling;
    Queue<KeyValuePair<ItemData, int>> entriesQueue = new Queue<KeyValuePair<ItemData, int>>();
    private void Start() {
        entriesPooling = new PoolingSystem<ItemPickupEntry>(itemPickupPrefab, 3, entriesParent);
    }

    public void AddItemInfo(ItemData item, int amount) {
        entriesQueue.Enqueue(new KeyValuePair<ItemData, int>(item, amount));
        ShowEntries();
    }

    void ShowEntries() {
        if (entriesQueue.Count == 0) return;

        StartCoroutine(ShowFromQueueCoroutine());
    }

    IEnumerator ShowFromQueueCoroutine() {
        ItemPickupEntry entry;
        KeyValuePair<ItemData, int> item;
        while(entriesQueue.Count > 0) {
            entry = entriesPooling.Get();
            item = entriesQueue.Dequeue();
            entry.Init(item.Key, item.Value);
            yield return new WaitForSeconds(1f);
        }
    }

    void FreeEntry(ItemPickupEntry entry) {
        entriesPooling.Disable(entry);
    }
}
