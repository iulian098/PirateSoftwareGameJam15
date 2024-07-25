using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    Dictionary<Enum_StatusEffectType, StatusEffect> statusEffects = new Dictionary<Enum_StatusEffectType, StatusEffect>(); //Used for threshold
    Dictionary<Enum_StatusEffectType, float> statusEffectsValue = new Dictionary<Enum_StatusEffectType, float>(); //Used as a timer

    public void Init() {
        
    }

    public void AddStatusEffect() {

    }
}
