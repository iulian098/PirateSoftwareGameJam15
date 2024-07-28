using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int health;
    protected bool isDead;

    public int MaxHealth { get => maxHealth;
        set { 
            maxHealth = value;
            health = value; 
        }
    }
    public int Health {
        get => health; 
        set {
            int oldHealth = health;
            health = value;
            if (health > maxHealth)
                health = maxHealth;
            OnHealthChanged?.Invoke(oldHealth, health);
        }
    }
    
    /// <summary>
    /// param1 OldHealth
    /// param2 NewHealth
    /// </summary>
    public Action<int, int> OnHealthChanged;
    public Action<int> OnDamageReceived;
    public Action OnDied;

    protected virtual void Awake() {
        health = maxHealth;
        isDead = false;
    }

    public virtual void ReceiveDamage(int dmg) {
        Health -= dmg;

        OnDamageReceived?.Invoke(dmg);

        if (health <= 0)
            Die();
    }

    public virtual void ReceiveDamage(WeaponData weapon) {
        Health -= weapon.Damage;

        OnDamageReceived?.Invoke(weapon.Damage);

        if(health <= 0) 
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
        Awake();
    }
}
