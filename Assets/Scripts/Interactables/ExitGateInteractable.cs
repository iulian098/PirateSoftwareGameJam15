using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGateInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] int creditsScene;
    public void OnInteract() {
        CommonPopup.Instance.Show("Are you sure you wanna leave? You did not obtain the plant you were looking for.", ShowDialog, "Yes", true, cancelButtonText: "No");
    }

    void ShowDialog() {
        DialogSystem.DialogSystem.Instance.ShowDialog("LeaveEnding");
        DialogSystem.DialogSystem.Instance.OnDialogEnd += ShowCredits;
    }

    void ShowCredits() {
        SceneManager.LoadScene("Credits");
    }
}
