using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteractable : MonoBehaviour, IInteractable {
    public void OnInteract() {
        CommonPopup.Instance.Show("An altar for the pantheon of Gods watching over the lands.\r\nWould you like to pray to them?", ChangePotions, "Yes", true, cancelButtonText:"No");
    }

    public void ChangePotions() {

    }
}
