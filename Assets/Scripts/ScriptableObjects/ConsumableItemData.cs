using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItemData", menuName = "Scriptable Objects/Items/ConsumableItemData")]
public class ConsumableItemData : ItemData
{
    [SerializeField] Enum_ModifyStat modifiedStat;
    [SerializeField] float amount;

    public Enum_ModifyStat ModifyStat => modifiedStat;
    public float Amount => amount;
}
