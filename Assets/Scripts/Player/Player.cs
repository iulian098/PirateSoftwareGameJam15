using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] PlayerController controller;
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] float pickupRadius;
    [SerializeField] Weapon weapon;

    PlayerInput playerInput => InGameManager.Instance.PlayerInput;

    private void Start() {
        HealthComponent.OnHealthChanged += OnHealthChanged;
        HealthComponent.OnDamageReceived += OnDamageReceived;
        UIManager.Instance.PlayerHealthBar.Init(HealthComponent.MaxHealth);
    }

    private void Update() {
        if (GlobalData.isPaused) return;

        if (playerInput.actions["Use"].WasPerformedThisFrame()) {
            Collider2D[] colls = new Collider2D[1];
            Physics2D.OverlapCircle(transform.position, pickupRadius, new ContactFilter2D() { layerMask = enemyLayerMask, useLayerMask = true, useTriggers = true }, colls);
            if(colls.Length > 0 && colls[0] != null) {
                if(colls[0].TryGetComponent<Enemy>(out Enemy enemy) && colls[0].TryGetComponent<ItemDrop>(out ItemDrop drop)) {
                    if (enemy.IsDead) {
                        drop.OnPickup();
                    }
                }
            }
        }
    }

    public void EquipWeapon(WeaponData weaponData) {
        weapon.ChangeWeapon(weaponData);
    }

    private void OnHealthChanged(int oldVal, int newVal) {
        UIManager.Instance.PlayerHealthBar.SetValue(oldVal, newVal);
    }

    private void OnDamageReceived(int dmg) {
        UIManager.Instance.DamageNumberManager.ShowText($"-{dmg}",
            false,
            Camera.main.WorldToScreenPoint(transform.position)
            );
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
