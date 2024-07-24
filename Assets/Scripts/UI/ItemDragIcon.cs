using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragIcon : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject itemDragObject;

    public void Show(Sprite icon) {
        itemIcon.sprite = icon;
        itemDragObject.SetActive(true);
    }

    public void Hide() {
        itemDragObject.SetActive(false);
    }
}
