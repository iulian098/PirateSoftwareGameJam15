using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellInteractable : MonoBehaviour, IInteractable {
    [SerializeField] ItemData item;
    [SerializeField] int maxAmount;
    [SerializeField] int amountPerInteraction;
    [SerializeField] SoundData sound;
    [SerializeField] ParticleSystem waterSplash;

    int currentAmount;

    private void Start() {
        currentAmount = maxAmount;
    }

    public void OnInteract() {
        if(currentAmount <= 0) {
            CommonPopup.Instance.Show("No more water left.");
            return;
        }

        if (currentAmount < amountPerInteraction)
            InventorySystem.Instance.AddItem(item, currentAmount);
        else
            InventorySystem.Instance.AddItem(item, amountPerInteraction);

        currentAmount -= amountPerInteraction;

        SoundManager.PlaySound(transform.position, sound);
        waterSplash.gameObject.SetActive(true);
    }
}
