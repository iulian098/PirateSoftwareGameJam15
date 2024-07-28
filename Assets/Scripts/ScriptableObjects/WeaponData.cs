using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")] 
public class WeaponData : ScriptableObject
{
    [SerializeField] Enum_WeaponType weaponType;
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] Enum_StatusEffectType applyStatusEffect;
    [SerializeField] AudioClip attackSound;

    public Enum_WeaponType WeaponType => weaponType;
    public int Damage => damage;
    public float FireRate => fireRate;
    public Enum_StatusEffectType ApplyStatusEffect => applyStatusEffect;
    public AudioClip AttackSound => attackSound;
}
