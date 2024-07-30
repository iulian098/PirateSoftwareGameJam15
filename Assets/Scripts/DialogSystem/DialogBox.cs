using UnityEngine;
using TMPro;

namespace DialogSystem {
    public class DialogBox : MonoBehaviour {
        [SerializeField] TMP_Text characterName;
        [SerializeField] TMP_Text text;
        DialogSystem dialogSystem;
        Dialog dialog;
        int dialogIndex = 0;

        public void Init(DialogSystem dialogSystem) {
            this.dialogSystem = dialogSystem;
        }

        public void ShowBox(Dialog dialog) {
            this.dialog = dialog;
            dialogIndex = 0;

            gameObject.SetActive(true);
            characterName.text = dialog.Dialogs[dialogIndex].CharacterName;
            text.text = dialog.Dialogs[dialogIndex].Text;
            dialog.Dialogs[dialogIndex].onShow?.Invoke();
            dialogSystem.OnDialogStart?.Invoke();
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
        }
    }
}