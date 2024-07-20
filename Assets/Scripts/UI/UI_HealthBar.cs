using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] Image newFillBar;
    [SerializeField] Image oldFillBar;
    [SerializeField] float minXSize;

    float maxWidth;
    int maxValue;

    bool valueChanged;
    Vector2 fillSizeTarget;
    Vector2 oldFillSizeTarget;


    void Start()
    {
        maxWidth = newFillBar.rectTransform.sizeDelta.x;
    }

    protected virtual void FixedUpdate() {

        if (valueChanged) {
            oldFillSizeTarget = oldFillBar.rectTransform.sizeDelta;
            oldFillSizeTarget.x = Mathf.Lerp(oldFillSizeTarget.x, newFillBar.rectTransform.sizeDelta.x, 0.1f);
            if(Mathf.Abs(oldFillSizeTarget.x - fillSizeTarget.x) < 0.1f) {
                oldFillSizeTarget.x = fillSizeTarget.x;
            }
            oldFillBar.rectTransform.sizeDelta = oldFillSizeTarget;

        }
    }

    public void Init(int maxVal) {
        maxValue = maxVal;
    }

    public void SetValue(int oldVal, int newVal) {
        fillSizeTarget = newFillBar.rectTransform.sizeDelta;
        float xSize = (float)maxWidth * ((float)newVal / maxValue);

        if (xSize < fillSizeTarget.x) {
            valueChanged = true;
        }
        else {
            oldFillSizeTarget = oldFillBar.rectTransform.sizeDelta;
            oldFillSizeTarget.x = fillSizeTarget.x;
            oldFillBar.rectTransform.sizeDelta = oldFillSizeTarget;
        }

        if (xSize < minXSize)
            fillSizeTarget.x = minXSize;
        else
            fillSizeTarget.x = xSize;


        newFillBar.rectTransform.sizeDelta = fillSizeTarget;
        
    }
}
