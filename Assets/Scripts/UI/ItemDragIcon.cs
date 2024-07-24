using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragIcon : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject itemDragObject;

    private void Update() {
        transform.position = MouseHelper.Instance.MousePos;
    }

    public void Show(Sprite icon) {
        transform.position = MouseHelper.Instance.MousePos;
        itemIcon.sprite = icon;
        itemDragObject.SetActive(true);
    }

    public void Hide() {
        itemDragObject.SetActive(false);
    }
}
