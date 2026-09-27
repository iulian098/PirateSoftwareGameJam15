using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Scriptable Objects/Weapons/MeleeWeaponData")]
public class MeleeWeaponData : WeaponData
{
    [SerializeField] float range;
    [SerializeField] VFX attackVfx;
    [SerializeField] CollisionDetectionType collisionDetectionType;

    public float Range => range;
    public VFX AttackVFX => attackVfx;
    public CollisionDetectionType CollisionDetectionType => collisionDetectionType;
}

public enum CollisionDetectionType
{
    None,
    Raycast,
    SphereCast
}
