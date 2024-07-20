using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthComponent healthComponent;

    public HealthComponent HealthComponent => healthComponent;
}
