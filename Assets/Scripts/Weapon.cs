using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform shootingPoint;

    bool isRanged;
    float fireRateTimer;
    Character character;
    ItemData itemData;
    RaycastHit2D raycastHit;

    public void Init(Character character) {
        this.character = character;
    }

    public void ChangeWeapon(WeaponData weaponData, ItemData itemData) {
        this.weaponData = weaponData;
        this.itemData = itemData;
        isRanged = weaponData.WeaponType == Enum_WeaponType.Ranged;
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

        if(!(itemData as EquipmentItemData).IsInfinite)
            InventorySystem.Instance.RemoveItem(itemData);

        SoundManager.PlaySound(transform.position, new SoundData {
            clip = weaponData.AttackSound != null ? weaponData.AttackSound : InGameManager.Instance.InGameData.DefaultAttackSound,
            maxDistance = 15,
            minDistance = 5,
            pitch = Random.Range(0.9f, 1.1f),
            volume = Random.Range(0.9f, 1f)
        });

        character.Animator.SetTrigger("Attack");

        if (isRanged) {
            Projectile proj = Instantiate((weaponData as RangeWeaponData).Projectile, shootingPoint.position, shootingPoint.rotation);
            proj.Init(shootingPoint.right, (weaponData as RangeWeaponData), weaponData.Damage);
        }
        else {
            raycastHit = Physics2D.Raycast(shootingPoint.position,
                shootingPoint.right,
                (weaponData as MeleeWeaponData).Range,
                InGameManager.Instance.InGameData.EnemyMask | InGameManager.Instance.InGameData.BreakableObjectMask | InGameManager.Instance.InGameData.InteractableMask
                );
            if(raycastHit.collider != null && raycastHit.collider.TryGetComponent<HealthComponent>(out var healthComp)){
                healthComp.ReceiveDamage(weaponData);
            }
        }

        fireRateTimer = weaponData.FireRate;
    }
}
