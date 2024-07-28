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

    InputAction useAction;

    PlayerInput playerInput => InGameManager.Instance.PlayerInput;

    private void Start() {
        HealthComponent.OnHealthChanged += OnHealthChanged;
        HealthComponent.OnDamageReceived += OnDamageReceived;
        HealthComponent.OnDied += OnDied;
        UIManager.Instance.PlayerHealthBar.Init(HealthComponent.MaxHealth);
        weapon.Init(this);
        useAction = playerInput.actions["Use"];
    }

    private void Update() {
        if (GlobalData.isPaused) return;

        Collider2D[] colls = new Collider2D[1];
        Physics2D.OverlapCircle(transform.position, pickupRadius, new ContactFilter2D() { layerMask = InGameManager.Instance.InGameData.PickupMask | InGameManager.Instance.InGameData.InteractableMask, useLayerMask = true, useTriggers = true }, colls);
        if (colls.Length > 0 && colls[0] != null) {
            if((InGameManager.Instance.InGameData.InteractableMask & (1 << colls[0].gameObject.layer)) != 0)
                UIManager.Instance.ShowInfoText( InfoTextStrings.UseString);
            else if((InGameManager.Instance.InGameData.PickupMask & (1 << colls[0].gameObject.layer)) != 0)
                UIManager.Instance.ShowInfoText(InfoTextStrings.PickupString);

            if (useAction.WasPerformedThisFrame()) {
                if (colls[0].TryGetComponent(out IInteractable drop))
                    drop.OnInteract();
            }
        }
        else {
            UIManager.Instance.HideInfo();
        }
    }

    private void OnDied() {
        anim.SetBool("Die", true);
    }

    public void EquipWeapon(WeaponData weaponData, ItemData itemData) {
        weapon.ChangeWeapon(weaponData, itemData);
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
