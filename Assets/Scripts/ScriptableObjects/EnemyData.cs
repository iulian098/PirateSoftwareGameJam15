using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] Sprite enemyIcon;
    [SerializeField] string enemyName;
    [SerializeField, Multiline] string enemyDescription;

    [SerializeField] int damage;
    [SerializeField] float attackRate;
    [SerializeField] DropData[] drops;
    [SerializeField] float defaultThresholdValue;
    [SerializeField] StatusEffect[] statusEffectsModifiers;

    public Sprite EnemyIcon => enemyIcon;
    public string EnemyName => enemyName;
    public string EnemyDescription => enemyDescription;
    public int Damage => damage;
    public float AttackRate => attackRate;
    public DropData[] Drops => drops;

}
