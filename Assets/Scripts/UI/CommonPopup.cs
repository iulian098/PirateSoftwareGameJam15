using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonPopup : MonoSingleton<CommonPopup>
{
    [SerializeField] GameObject contents;

    [SerializeField] TMP_Text body;

    [SerializeField] Button okButton;
    [SerializeField] Button cancelButton;

    public void Show(string bodyText, UnityAction okAction = null, bool showCancel = false, UnityAction cancelAction = null) {
        contents.SetActive(true);
        body.text = bodyText;

        cancelButton.gameObject.SetActive(showCancel);

        if (okButton != null && okAction != null) {
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(okAction);
        }

        if(cancelButton != null && cancelAction != null) {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(cancelAction);
        }
    }

    public void Hide() {
        contents.SetActive(false);
    }
}
