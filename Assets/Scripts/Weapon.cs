using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform shootingPoint;

    float fireRateTimer;

    public void ChangeWeapon(WeaponData weaponData) {
        this.weaponData = weaponData;
    }

    private void FixedUpdate() {
        if (fireRateTimer > 0)
            fireRateTimer -= Time.deltaTime;
    }

    public virtual void Attack() {
        if(weaponData == null) {
            Debug.LogError("Null weapon data");
            return;
        }

        if (fireRateTimer > 0) return;
        Projectile proj = Instantiate(weaponData.Projectile, shootingPoint.position, shootingPoint.rotation);
        proj.Init(shootingPoint.right, weaponData.ProjectileConfig);
        fireRateTimer = weaponData.FireRate;
    }
}
