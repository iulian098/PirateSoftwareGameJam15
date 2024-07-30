using System;
using UnityEngine;

namespace DialogSystem {

    public class DialogSystem : MonoSingleton<DialogSystem> {

        [SerializeField] DialogContainer container;
        [SerializeField] DialogBox dialogBox;
        public Action OnDialogStart;
        public Action OnDialogEnd;

        private void Start() {
            dialogBox.Init(this);
        }

        public void ShowDialog(string name, Action onDialogStart = null, Action onDialogEnd = null) {
            Dialog diag = container.GetDialogByName(name);
            if (diag == null) {
                Debug.LogError("No dialog with name " + name + " found");
                return;
            }
            OnDialogStart = onDialogStart;
            OnDialogEnd = onDialogEnd;
            dialogBox.ShowBox(diag);
        }
    }
}
