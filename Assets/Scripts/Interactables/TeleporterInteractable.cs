using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterInteractable : MonoBehaviour, IInteractable {
    [SerializeField] int levelIndex;
    [SerializeField, Multiline] string popupText;
    public void OnInteract() {
        CommonPopup.Instance.Show(popupText, GoToLevel, "Yes", true, cancelButtonText: "No");
    }

    void GoToLevel() {
        UserManager.playerData.SetInt("LastLevel", levelIndex);
        SceneManager.LoadScene(levelIndex);
    }
}
