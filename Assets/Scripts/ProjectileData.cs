using UnityEngine;

[System.Serializable]
public class ProjectileData {
    public float speed;
    public float maxDistance;
    public GameObject hitVFX;
    public AudioClip[] hitSound;
    public bool useAdditionalSounds;
    public AudioClip[] additionalHitSounds;
}