using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponData", menuName = "Scriptable Objects/Weapons/RangeWeaponData")]
public class RangeWeaponData : WeaponData
{
    [SerializeField] Projectile projectile;
    [SerializeField] ProjectileData projectileConfig;

    public Projectile Projectile => projectile;
    public ProjectileData ProjectileConfig => projectileConfig;

}
