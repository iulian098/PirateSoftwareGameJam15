using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterInteractable : MonoBehaviour, IInteractable {
    [SerializeField] int levelIndex;
    [SerializeField] string popupText;
    public void OnInteract() {
        CommonPopup.Instance.Show(popupText, GoToLevel, "Yes", true, cancelButtonText: "No");
    }

    void GoToLevel() {
        SceneManager.LoadScene(levelIndex);
    }
}
