using TMPro;
using UnityEngine;

public class UI_NumberText : MonoBehaviour
{
    [SerializeField] TMP_Text textComp;
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color greaterColor = Color.green;
    [SerializeField] Color lesserColor = Color.red;

    public void SetText(string text) {
        textComp.text = text;
        textComp.color = defaultColor;
    }

    public void SetText(string text, bool isGreater) {
        textComp.text = text;
        textComp.color = isGreater ? greaterColor : lesserColor;
    }
}
