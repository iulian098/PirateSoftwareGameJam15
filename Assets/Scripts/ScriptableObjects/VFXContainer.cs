using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VFXContainer", menuName = "Scriptable Objects/VFX Container")]
public class VFXContainer : ScriptableObject
{
    [SerializeField] VFX[] vfxs;

    public Dictionary<string, VFX> vfxDictionary = new Dictionary<string, VFX>();

    public Dictionary<string, VFX> VFX_Dictionary {
        get {
            if (vfxDictionary.Count != vfxs.Length) {
                vfxDictionary.Clear();
                foreach (var vfx in vfxs)
                    vfxDictionary.Add(vfx.VFXName, vfx);
            }

            return vfxDictionary;
        }
    }
}
