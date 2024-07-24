using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{
    [SerializeField] GameObject bottleShardsVfx;

    public GameObject BottleShardsVFX => bottleShardsVfx;
}
