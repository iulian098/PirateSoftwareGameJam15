using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] Projectile projectile;
    [SerializeField] float fireRate;

    float lastShootTime;
    public virtual void Attack() {
        if (Time.time - lastShootTime < fireRate) return;
        Projectile proj = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        proj.Init(shootingPoint.right);
        Debug.Log("Pew"); 
        lastShootTime = Time.time;
    }
}
