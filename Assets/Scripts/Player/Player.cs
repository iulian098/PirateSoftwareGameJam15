using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] PlayerController controller;

    private void Start() {
        HealthComponent.OnHealthChanged += OnHealthChanged;
        HealthComponent.OnDamageReceived += OnDamageReceived;
        UIManager.Instance.PlayerHealthBar.Init(HealthComponent.MaxHealth);
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
}
