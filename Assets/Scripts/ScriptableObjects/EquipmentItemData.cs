using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItemData", menuName = "Scriptable Objects/Items/EquipmentItemData")]
public class EquipmentItemData : ItemData
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] bool isInfinite;

    public WeaponData WeaponData => weaponData;
    public bool IsInfinite => isInfinite;
}
