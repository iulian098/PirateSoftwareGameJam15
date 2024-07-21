using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int health;
    protected bool isDead;

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
        isDead = false;
    }

    public virtual void ReceiveDamage(int dmg) {
        Health -= dmg;

        OnDamageReceived?.Invoke(dmg);

        if (health <= 0)
            Die();
    }

    protected virtual void Die() {
        OnDied?.Invoke();
        isDead = true;
        ClearActions();
    }

    public void ClearActions() {
        OnHealthChanged = null;
        OnDamageReceived = null;
        OnDied = null;
    }

    public void ClearAll() {
        ClearActions();
        Start();
    }
}
