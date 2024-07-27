using UnityEngine;

public struct SoundData {
    public AudioClip clip;
    public float volume;
    public float pitch;
    public float maxDistance;
    public float minDistance;
}

public class SoundManager
{
    public static void PlaySound(Vector2 position, SoundData data) {
        GameObject go = new GameObject(data.clip != null ? data.clip.name : "New AudioSource");
        go.transform.position = position;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = data.clip;
        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.volume = data.volume;
        audioSource.pitch = data.pitch;
        audioSource.maxDistance = data.maxDistance;
        audioSource.minDistance = data.minDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();
        Object.Destroy(go, data.clip.length);
    }

}
