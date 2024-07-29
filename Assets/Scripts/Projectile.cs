using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;
    [SerializeField] float maxDistance;
    
    [SerializeField] GameObject hitVFX;
    [SerializeField] Rigidbody2D rb;

    int damage;
    Vector2 startPos;
    RangeWeaponData weaponData;
    GameObject caster;

    public void Init(GameObject caster, Vector2 forceDirection, int damage)
    {
        startPos = transform.position;
        this.damage = damage;
        rb.AddForce(forceDirection * speed);
        this.caster = caster;
    }

    public void Init(GameObject caster, Vector2 forceDirection, RangeWeaponData weaponData, int damage) {
        this.weaponData = weaponData;
        speed = weaponData.ProjectileConfig.speed;
        maxDistance = weaponData.ProjectileConfig.maxDistance;
        hitVFX = weaponData.ProjectileConfig.hitVFX;
        startPos = transform.position;
        this.damage = damage;
        this.caster = caster;
        rb.AddForce(forceDirection * speed);
        Debug.Log(transform.position);
    }

    private void FixedUpdate() {
        if (Vector2.Distance(startPos, transform.position) > maxDistance)
            OnHit(null);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnHit(collision);
    }

    protected virtual void OnHit(Collider2D coll) {
        if (coll != null) {
            //Give damage
            if (coll.CompareTag("Enemy") || coll.CompareTag("Player")) {
                if (coll.gameObject == caster) return;
                Character character = coll.GetComponent<Character>();
                if (character != null)
                    character.ReceiveDamage(weaponData);
                else
                    Debug.LogWarning("Health Component not found");
            }
            else if (coll.CompareTag("BreakableProp")) {
                BreakableProp prop = coll.GetComponent<BreakableProp>();
                if (prop != null)
                    prop.ReceiveDamage(weaponData);
            }
        }
        SoundManager.Instance.PlaySound(transform.position, weaponData.ProjectileConfig.hitSoundName);

        if (weaponData.ProjectileConfig.useAdditionalSounds) {
            foreach (var additionalSound in weaponData.ProjectileConfig.additionalHitSoundData)
                SoundManager.PlaySound(transform.position, additionalSound);
        }

        Destroy(gameObject);
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Instantiate(InGameManager.Instance.InGameData.BottleShardsVFX, transform.position, Quaternion.identity);
    }
}
