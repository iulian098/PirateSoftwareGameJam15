using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    protected UI_HealthBar bossHealthBar;
    protected Dictionary<string, object> customData = new Dictionary<string, object>();
    bool activated;

    public bool Activated {
        get => activated;
        set {
            if (value)
                bossHealthBar.gameObject.SetActive(true);
            activated = value;
        }
    }

    protected override void Start() {
        lastX = transform.position.x;
        spriteXScale = characterSprite.transform.localScale.x;

        healthComponent.MaxHealth = enemyData.Health;
        bossHealthBar = UIManager.Instance.EnemyHealthBarManager.BossHealthBar;
        bossHealthBar.Init(healthComponent.MaxHealth);
        bossHealthBar.SetValue(healthComponent.MaxHealth, healthComponent.Health);
        HealthComponent.OnHealthChanged += bossHealthBar.SetValue;
        HealthComponent.OnDamageReceived += OnDamageReceived;
    }

    private void OnDamageReceived(int dmg) {
        UIManager.Instance.DamageNumberManager.ShowText($"-{dmg}",
            false,
            Camera.main.WorldToScreenPoint(transform.position)
            );
    }


    protected override void FixedUpdate() {
        if (IsDead || Target == null) return;

        base.FixedUpdate();
    }

    public object GetCustomData(string key) {
        if (customData.TryGetValue(key, out object result))
            return result;
        return null;
    }

    public void SetCustomData(string key, object val) {
        if(customData.ContainsKey(key))
            customData[key] = val;
        else
            customData.Add(key, val);
    }

    public void SetPlayerTarget(Player target) {
        this.target = target;
    }
}
