using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform shootingPoint;

    float fireRateTimer;
    Character character;
    ItemData itemData;


    public void Init(Character character) {
        this.character = character;
    }

    public void ChangeWeapon(WeaponData weaponData, ItemData itemData) {
        this.weaponData = weaponData;
        this.itemData = itemData;
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
        if (!InventorySystem.Instance.InventoryContainer.ItemsIDs.Contains(itemData.ID)) {
            ChangeWeapon(null, null);
            return;
        }
        InventorySystem.Instance.RemoveItem(itemData);

        character.Animator.SetTrigger("Attack");

        Projectile proj = Instantiate(weaponData.Projectile, shootingPoint.position, shootingPoint.rotation);
        proj.Init(shootingPoint.right, weaponData, weaponData.Damage);
        fireRateTimer = weaponData.FireRate;
    }
}
