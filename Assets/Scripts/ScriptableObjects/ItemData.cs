using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/Items/New Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] protected int id;
    [SerializeField] Enum_ItemType type;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string itemName;
    [SerializeField, Multiline] protected string itemDescription;

    public int ID => id;
    public Enum_ItemType Type => type;
    public Sprite Icon => icon;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
}
