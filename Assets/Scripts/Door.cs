using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openedSprite;
    [SerializeField] Collider2D coll;
    [SerializeField] ItemData requiredItem;
    [SerializeField] GameObject opened;
    [SerializeField] GameObject closed;
    [SerializeField] string noItemDialogName;
    [SerializeField] string openAudio;

    public void OnInteract() {
        if (InGameManager.Instance.InventoryContainer.ItemsIDs.Contains(requiredItem.ID)) {
            //spriteRenderer.sprite = openedSprite;
            opened.SetActive(true);
            closed.SetActive(false);
            coll.enabled = false;
            gameObject.layer = 0;
            
            SoundManager.Instance.PlaySound(transform.position, openAudio == string.Empty ? "DoorOpen" : openAudio);
        }
        else {
            DialogSystem.DialogSystem.Instance.ShowDialog(noItemDialogName);
        }
    }

}
