using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{
    [Header("Audio")]
    [SerializeField] AudioClip defaultAttackSound;

    [Header("Layers")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask pickupMask;
    [SerializeField] LayerMask breakableObjectMask;
    [SerializeField] LayerMask interactableMask;

    [SerializeField] GameObject bottleShardsVfx;
    [SerializeField] GameObject deathVfx;
    [SerializeField] ItemDrop dropPrefab;
    [SerializeField] UI_ItemPickupInfo pickupInfo;

    public AudioClip DefaultAttackSound => defaultAttackSound;

    public LayerMask BreakableObjectMask => breakableObjectMask;
    public LayerMask PlayerMask => playerMask;
    public LayerMask EnemyMask => enemyMask;
    public LayerMask PickupMask => pickupMask;
    public LayerMask InteractableMask => interactableMask;

    public GameObject BottleShardsVFX => bottleShardsVfx;
    public GameObject DeathVFX => deathVfx;
    public ItemDrop DropPrefab => dropPrefab;
    public UI_ItemPickupInfo PickupInfo => pickupInfo;
}
