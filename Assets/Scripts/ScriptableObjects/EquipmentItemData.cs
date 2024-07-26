using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItemData", menuName = "Scriptable Objects/EquipmentItemData")]
public class EquipmentItemData : ItemData
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] bool isInfinite;
    [SerializeField] bool inInventoryByDefault;

    public WeaponData WeaponData => weaponData;
    public bool IsInfinite => isInfinite;
}
