using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterInteractable : MonoBehaviour, IInteractable {
    [SerializeField] int levelIndex;
    public void OnInteract() {
        CommonPopup.Instance.Show("Proceed to the dungeon?", GoToLevel, "Yes", true, cancelButtonText: "No");
    }

    void GoToLevel() {
        SceneManager.LoadScene(levelIndex);
    }
}
