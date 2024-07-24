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

    public void Init(Vector2 forceDirection)
    {
        startPos = transform.position;
        rb.AddForce(forceDirection * speed);
    }

    public void Init(Vector2 forceDirection, WeaponData.ProjectileData projectileData, int damage) {
        speed = projectileData.speed;
        maxDistance = projectileData.maxDistance;
        hitVFX = projectileData.hitVFX;
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
            HealthComponent healthComp = coll.GetComponent<HealthComponent>();
            if (healthComp != null)
                healthComp.ReceiveDamage(damage);
            else
                Debug.LogWarning("Health Component not found");
        }
        Destroy(gameObject);
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Instantiate(InGameManager.Instance.InGameData.BottleShardsVFX, transform.position, Quaternion.identity);
    }
}
