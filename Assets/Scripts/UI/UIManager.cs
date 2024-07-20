using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] UI_HealthBar playerHealthBar;
    [SerializeField] EnemyHealthBarManager enemyHealthBarManager;
    [SerializeField] UI_DamageNumberManager damageNumberManager;

    public UI_HealthBar PlayerHealthBar => playerHealthBar;
    public EnemyHealthBarManager EnemyHealthBarManager => enemyHealthBarManager;
    public UI_DamageNumberManager DamageNumberManager => damageNumberManager;
}
