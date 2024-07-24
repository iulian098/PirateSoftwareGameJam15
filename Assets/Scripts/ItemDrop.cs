using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject dropVfx;

    DropData[] itemDrops;
    bool itemPickedUp;
    GameObject spawnedVfx;
    List<DropData> droppedItems = new List<DropData>();

    private void Start() {
        enemy.HealthComponent.OnDied += CheckDrop;
        itemDrops = enemy.EnemyData.Drops;
    }

    public void CheckDrop() {
        foreach (var item in itemDrops) {
            int drop = Random.Range(0, 101);
            if(drop <= item.chance) {
                droppedItems.Add(item);
            }
        }

        if (droppedItems.Count > 0) {
            spawnedVfx = Instantiate(dropVfx, transform.position, Quaternion.identity);
            spawnedVfx.transform.SetParent(transform);
        }
    }

    public void OnPickup() {
        if (itemPickedUp) return;

        List<DropData> pickedUp = new List<DropData>();

        foreach (var item in droppedItems) {
            if(InventorySystem.Instance.AddItem(item.item, item.amount))
                pickedUp.Add(item);
            else
                break;
        }

        for (int i = 0; i < pickedUp.Count; i++)
            droppedItems.Remove(pickedUp[i]);

        pickedUp.Clear();

        if (droppedItems.Count == 0) {
            itemPickedUp = true;
            Destroy(spawnedVfx.gameObject);
        }
    }


}
