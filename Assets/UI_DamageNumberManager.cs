using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DamageNumberManager : MonoBehaviour
{
    [SerializeField] UI_NumberText textPrefab;
    [SerializeField] Transform parent;

    PoolingSystem<UI_NumberText> numbersPool;

    private void Start() {
        if(numbersPool == null)
            numbersPool = new PoolingSystem<UI_NumberText>(textPrefab, 5, parent);
    }

    public void ShowText(string text, Vector3 position) {
        UI_NumberText obj = numbersPool.Get();
        obj.SetText(text);
        obj.transform.position = position;
        StartCoroutine(TextTimer(obj));
    }

    public void ShowText(string text, bool isGreater, Vector3 position) {
        UI_NumberText obj = numbersPool.Get();
        obj.SetText(text, isGreater);
        obj.transform.position = position;
        StartCoroutine(TextTimer(obj));
    }

    IEnumerator TextTimer(UI_NumberText obj) {
        yield return new WaitForSeconds(2);
        numbersPool.Disable(obj);
    }

}
