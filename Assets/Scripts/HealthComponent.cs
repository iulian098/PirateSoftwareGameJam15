using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int health;

    public int MaxHealth => maxHealth;
    public int Health {
        get => health; 
        set {
            OnHealthChanged?.Invoke(health, value);
            health = value;
        }
    }

    /// <summary>
    /// param1 OldHealth
    /// param2 NewHealth
    /// </summary>
    public Action<int, int> OnHealthChanged;
    public Action<int> OnDamageReceived;
    public Action OnDied;

    protected virtual void Start() {
        health = maxHealth;
    }

    public virtual void ReceiveDamage(int dmg) {
        Health -= dmg;

        OnDamageReceived?.Invoke(health);

        if (health <= 0)
            Die();
    }

    protected virtual void Die() {
        OnDied?.Invoke();
    }
}
