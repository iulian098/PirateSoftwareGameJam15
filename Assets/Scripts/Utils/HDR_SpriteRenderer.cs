using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HDR_SpriteRenderer : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color32 hdrColor;

    private void Start() {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetColor("_HDR", hdrColor);
    }

#if UNITY_EDITOR
    private void Update() {
        if (spriteRenderer != null)
            spriteRenderer.material.SetColor("_HDR", hdrColor);
    }
#endif
}
