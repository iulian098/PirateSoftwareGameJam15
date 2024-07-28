using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Scriptable Objects/Weapons/MeleeWeaponData")]
public class MeleeWeaponData : WeaponData
{
    [SerializeField] float range;


    public float Range => range;
}
