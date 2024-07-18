using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/New Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] string itemName;
    [SerializeField, Multiline] string itemDescription;

    public Sprite Icon => icon;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
}
