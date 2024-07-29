using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ShowDialog : MonoBehaviour, IInteractable {
    [SerializeField] string dialogName;
    public void OnInteract() {
        DialogSystem.DialogSystem.Instance.ShowDialog(dialogName);
    }
}
