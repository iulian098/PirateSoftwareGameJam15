using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] PlayerController controller;

    private void Start() {
        HealthComponent.OnHealthChanged += OnHealthChanged;
        UIManager.Instance.PlayerHealthBar.Init(HealthComponent.MaxHealth);
    }

    private void OnHealthChanged(int oldVal, int newVal) {
        UIManager.Instance.PlayerHealthBar.SetValue(oldVal, newVal);
    }
}
