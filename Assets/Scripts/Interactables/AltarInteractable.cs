using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteractable : MonoBehaviour, IInteractable {
    [SerializeField] ItemData requiredItem;
    [SerializeField] ItemData changeIn;
    public void OnInteract() {
        CommonPopup.Instance.Show("An altar for the pantheon of Gods watching over the lands.\r\nWould you like to pray to them?", ChangePotions, "Yes", true, cancelButtonText:"No");
    }

    public void ChangePotions() {
        if (!InGameManager.Instance.InventoryContainer.ItemsIDs.Contains(requiredItem.ID)) {
            CommonPopup.Instance.Show($"You don't have {requiredItem.ItemName} in your inventory");
            return;
        }

        int itemIndex = InGameManager.Instance.InventoryContainer.ItemsIDs.IndexOf(requiredItem.ID);
        int amount = InGameManager.Instance.InventoryContainer.Amounts[itemIndex];

        InGameManager.Instance.InventoryContainer.RemoveItem(requiredItem.ID, amount);
        InGameManager.Instance.InventoryContainer.AddItem(changeIn, amount);

        CommonPopup.Instance.Show($"Your {requiredItem.ItemName} have turned into {changeIn.ItemName}");

    }
}
