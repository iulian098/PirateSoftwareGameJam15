using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProp : HealthComponent
{
    [SerializeField] List<WeaponData> receiveDamageFrom = new List<WeaponData>();
    [SerializeField] List<DropData> drops = new List<DropData>();
    [SerializeField] SoundData receiveDamageSound;
    [SerializeField] SoundData destroySound;

    private void Start() {
        OnDied += OnDestroyed;
        OnDamageReceived += OnDamaged;
    }

    private void OnDamaged(int obj) {
        SoundManager.PlaySound(transform.position, receiveDamageSound);
    }

    public override void ReceiveDamage(WeaponData weapon) {
        if (!receiveDamageFrom.Contains(weapon)) return;

        base.ReceiveDamage(weapon);
    }

    private void OnDestroyed() {

        List<DropData> droppedItems = new List<DropData>();

        foreach (var item in drops) {
            int dropC = UnityEngine.Random.Range(0, 101);
            if (dropC <= item.chance) {
                droppedItems.Add(item);
            }
        }
        if (droppedItems.Count > 0) {
            ItemDrop drop = Instantiate(InGameManager.Instance.InGameData.DropPrefab, transform.position, Quaternion.identity);
            drop.Init(droppedItems);
        }
        SoundManager.PlaySound(transform.position, destroySound);
        Instantiate(InGameManager.Instance.InGameData.DeathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
