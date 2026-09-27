using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace DialogSystem {
    public class DialogBox : MonoBehaviour {
        [SerializeField] TMP_Text characterName;
        [SerializeField] TMP_Text text;

        InputAction anyKeyAction;
        DialogSystem dialogSystem;
        Dialog dialog;
        int dialogIndex = 0;
        bool isOpen = false;

        public void Init(DialogSystem dialogSystem) {
            this.dialogSystem = dialogSystem;
            if(anyKeyAction == null)
            {
                anyKeyAction = InGameManager.Instance.PlayerInput.actions["AnyKey"];
                anyKeyAction.started += AnyKeyAction_started;
            }
        }

        private void OnDestroy()
        {
            if(anyKeyAction != null)
                anyKeyAction.started -= AnyKeyAction_started;
        }

        private void AnyKeyAction_started(InputAction.CallbackContext obj)
        {
            if (!isOpen) 
                return;
            ShowNext();
        }

        public void ShowBox(Dialog dialog) {
            this.dialog = dialog;
            dialogIndex = 0;

            gameObject.SetActive(true);
            characterName.text = dialog.Dialogs[dialogIndex].CharacterName;
            text.text = dialog.Dialogs[dialogIndex].Text;
            dialog.Dialogs[dialogIndex].onShow?.Invoke();
            dialogSystem.OnDialogStart?.Invoke();
            isOpen = true;
        }

        public void ShowNext() {
            dialogIndex++;

            if(dialogIndex >= dialog.Dialogs.Length) {
                dialogSystem.OnDialogEnd?.Invoke();
                HideBox();
                return;
            }

            characterName.text = dialog.Dialogs[dialogIndex].CharacterName;
            text.text = dialog.Dialogs[dialogIndex].Text;
            dialog.Dialogs[dialogIndex].onShow?.Invoke();
        }

        public void HideBox() {
            gameObject.SetActive(false);
            isOpen = false;
        }
    }
}