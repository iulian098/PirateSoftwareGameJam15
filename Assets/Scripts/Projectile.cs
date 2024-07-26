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
    WeaponData weaponData;

    public void Init(Vector2 forceDirection)
    {
        startPos = transform.position;
        rb.AddForce(forceDirection * speed);
    }

    public void Init(Vector2 forceDirection, WeaponData weaponData, int damage) {
        this.weaponData = weaponData;
        speed = weaponData.ProjectileConfig.speed;
        maxDistance = weaponData.ProjectileConfig.maxDistance;
        hitVFX = weaponData.ProjectileConfig.hitVFX;
        startPos = transform.position;
        this.damage = damage;
        
        rb.AddForce(forceDirection * speed);
    }

    private void FixedUpdate() {
        if (Vector2.Distance(startPos, transform.position) > maxDistance)
            OnHit(null);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnHit(collision);
    }

    private void OnHit(Collider2D coll) {
        if (coll != null && coll.CompareTag("Enemy")) {
            //Give damage
            Character character = coll.GetComponent<Character>();
            if (character != null)
                character.ReceiveDamage(weaponData);
            else
                Debug.LogWarning("Health Component not found");
        }
        SoundManager.PlaySound(transform.position, new SoundData {
            clip = weaponData.ProjectileConfig.hitSound.Length != 0 ? weaponData.ProjectileConfig.hitSound[UnityEngine.Random.Range(0, weaponData.ProjectileConfig.hitSound.Length)] : null,
            maxDistance = 15,
            minDistance = 5,
            pitch = UnityEngine.Random.Range(0.9f, 1.1f),
            volume = UnityEngine.Random.Range(0.1f, 0.2f)
        });

        if (weaponData.ProjectileConfig.useAdditionalSounds) {
            SoundManager.PlaySound(transform.position, new SoundData {
                clip = weaponData.ProjectileConfig.hitSound.Length != 0 ? 
                    weaponData.ProjectileConfig.additionalHitSounds[UnityEngine.Random.Range(0, weaponData.ProjectileConfig.additionalHitSounds.Length)] : null,
                maxDistance = 15,
                minDistance = 5,
                pitch = UnityEngine.Random.Range(0.9f, 1.1f),
                volume = 0.3f
            });

        }

        Destroy(gameObject);
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Instantiate(InGameManager.Instance.InGameData.BottleShardsVFX, transform.position, Quaternion.identity);
    }
}
