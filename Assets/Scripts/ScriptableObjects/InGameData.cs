using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask pickupMask;

    [SerializeField] GameObject bottleShardsVfx;
    [SerializeField] GameObject deathVfx;
    [SerializeField] ItemDrop dropPrefab;

    public LayerMask PlayerMask => playerMask;
    public LayerMask EnemyMask => enemyMask;
    public LayerMask PickupMask => pickupMask;

    public GameObject BottleShardsVFX => bottleShardsVfx;
    public GameObject DeathVFX => deathVfx;
    public ItemDrop DropPrefab => dropPrefab;
}
