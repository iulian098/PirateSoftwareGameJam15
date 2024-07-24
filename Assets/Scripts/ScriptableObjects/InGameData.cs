using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{
    [SerializeField] GameObject bottleShardsVfx;
    [SerializeField] GameObject deathVfx;

    public GameObject BottleShardsVFX => bottleShardsVfx;
    public GameObject DeathVFX => deathVfx;
}
