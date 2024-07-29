using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class VFXManager : MonoBehaviour {
    [SerializeField] VFXContainer container;

    static Dictionary<string, PoolingSystem<VFX>> pools = new Dictionary<string, PoolingSystem<VFX>>();
    static Dictionary<string, VFX> vfxRefs = new Dictionary<string, VFX>();
    private void Awake() {
        DontDestroyOnLoad(this);
        vfxRefs = container.VFX_Dictionary;
    }

    public static VFX ShowVFX(string vfxName, Vector3 position, Quaternion rotation, Transform parent = null) {

        if (!vfxRefs.TryGetValue(vfxName, out var vfx)) {
            Debug.LogError($"VFX {vfxName} not found");
            return null;
        }

        if (!pools.TryGetValue(vfxName, out var vfxPool)) {
            pools.Add(vfxName, new PoolingSystem<VFX>(vfx, 1, parent));
        }

        VFX shownVFX = vfxPool.Get(position, rotation);
        shownVFX.Show(OnHide);
        return shownVFX;
    }

    static void OnHide(VFX vfx) {
        if (!pools.TryGetValue(vfx.VFXName, out var vfxPool)) {
            Debug.LogError($"Failed to disable {vfx.name}", vfx);
            return;
        }
        vfxPool.Disable(vfx);
    }
}