using UnityEngine;

[System.Serializable]
public class ProjectileData {
    public float speed;
    public float maxDistance;
    public GameObject hitVFX;
    public string hitSoundName;
    public bool useAdditionalSounds;
    public SoundData[] additionalHitSoundData;
}