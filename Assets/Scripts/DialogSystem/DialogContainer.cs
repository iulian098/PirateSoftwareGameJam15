using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem {

    [CreateAssetMenu(fileName = "DialogContainer", menuName = "Scriptable Objects/Dialog Container")]
    public class DialogContainer : ScriptableObject {
        [SerializeField] Dialog[] dialogs;

        public Dialog GetDialogByName(string name) {
            for (int i = 0; i < dialogs.Length; i++) {
                if (dialogs[i].Name == name)
                    return dialogs[i];
            }

            Debug.LogError($"No dialog with name {name} has found");

            return null;
        }
    }

    [System.Serializable]
    public class Dialog {
        [SerializeField] string name;
        [SerializeField] DialogText[] dialogs;
        public string Name => name;
        public DialogText[] Dialogs => dialogs;
    }

    [System.Serializable]
    public class DialogText {
        [SerializeField] string name;
        [SerializeField][TextArea] string text;
        public UnityEvent onShow;

        public string Name => name;
        public string Text => text;
    }

}