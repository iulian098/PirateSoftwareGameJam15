using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;

    public void Show(ItemData item, Vector2 position) {
        transform.position = position;
        titleText.text = item.ItemName;
        descriptionText.text = item.ItemDescription;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
