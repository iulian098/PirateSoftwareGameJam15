using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragIcon : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject itemDragObject;

    public void Show(Sprite icon, Vector3 startPosition) {
        transform.position = startPosition;
        itemIcon.sprite = icon;
        itemDragObject.SetActive(true);
    }

    public void Hide() {
        itemDragObject.SetActive(false);
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}
