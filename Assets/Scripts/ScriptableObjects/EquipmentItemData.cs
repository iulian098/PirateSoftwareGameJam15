using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItemData", menuName = "Scriptable Objects/EquipmentItemData")]
public class EquipmentItemData : ItemData
{
    [SerializeField] WeaponData weaponData;

    public WeaponData WeaponData => weaponData;
}
