using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Rigidbody2D rb;

    public void Init(Vector2 forceDirection)
    {
        rb.AddForce(forceDirection * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            //Give damage
        }
        Destroy(gameObject);
        Instantiate(hitVFX, transform.position, Quaternion.identity);
    }
}
