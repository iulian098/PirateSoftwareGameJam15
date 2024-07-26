using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")] 
public class WeaponData : ScriptableObject
{
    [System.Serializable]
    public class ProjectileData {
        public float speed;
        public float maxDistance;
        public GameObject hitVFX;
        public AudioClip[] hitSound;
        public bool useAdditionalSounds;
        public AudioClip[] additionalHitSounds;
    }

    [SerializeField] Projectile projectile;
    [SerializeField] ProjectileData projectileConfig;
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] Enum_StatusEffectType applyStatusEffect;
    [SerializeField] AudioClip attackSound;
    

    public Projectile Projectile => projectile;
    public ProjectileData ProjectileConfig => projectileConfig;
    public int Damage => damage;
    public float FireRate => fireRate;
    public Enum_StatusEffectType ApplyStatusEffect => applyStatusEffect;
    public AudioClip AttackSound => attackSound;
}
