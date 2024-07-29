using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] DropData[] items;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closeSprite;
    [SerializeField] Sprite openSprite;
    [SerializeField] GameObject vfx;

    List<DropData> droppedItems = new List<DropData>();
    bool isOpen;

    public void OnInteract() {
        if (!isOpen)
            OpenChest();
        else
            Collect();
    }

    void OpenChest(){
        isOpen = true;
        spriteRenderer.sprite = openSprite;
        vfx.SetActive(true);

        foreach (var item in items) {
            int dropC = Random.Range(0, 101);
            if (dropC <= item.chance)
                droppedItems.Add(item);
        }

        SoundManager.Instance.PlaySound(transform.position, "ChestOpen");
    }

    void Collect() {
        foreach (var item in droppedItems) {
            InventorySystem.Instance.AddItem(item.item, item.amount);
                //UIManager.Instance.ShowPickupInfo(item.item, item.amount);
        }
        gameObject.layer = 0;
        vfx.SetActive(false);
        SoundManager.Instance.PlaySound(Vector3.zero, "ItemPickup");

    }
}
