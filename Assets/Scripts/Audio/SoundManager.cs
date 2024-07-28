using UnityEngine;

[System.Serializable]
public class SoundData {
    public string name;
    public AudioClip[] clip;
    public float volumeMin;
    public float volumeMax;
    public float pitchMin;
    public float pitchMax;
    public float maxDistance;
    public float minDistance;
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] SoundsContainer soundsContainer;
    public static void PlaySound(Vector2 position, SoundData data) {
        if (data.clip.Length == 0) return;
        AudioClip clip = data.clip.Length > 1 ? data.clip[Random.Range(0, data.clip.Length)] : data.clip[0];
        GameObject go = new GameObject(data.clip != null ? clip.name : "New AudioSource");
        go.transform.position = position;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.volume = Random.Range(data.volumeMin, data.volumeMax);
        audioSource.pitch = Random.Range(data.pitchMin, data.pitchMax);
        audioSource.maxDistance = data.maxDistance;
        audioSource.minDistance = data.minDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();
        Destroy(go, clip.length);
    }

    public AudioSource PlaySound(Vector2 position, string soundName) {
        SoundData data = soundsContainer.GetSound(soundName);
        if (data == null) return null;
        AudioClip clip = data.clip.Length > 1 ? data.clip[Random.Range(0, data.clip.Length)] : data.clip[0];
        GameObject go = new GameObject(data.clip != null ? clip.name : "New AudioSource");
        go.transform.position = position;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.volume = Random.Range(data.volumeMin, data.volumeMax);
        audioSource.pitch = Random.Range(data.pitchMin, data.pitchMax);
        audioSource.maxDistance = data.maxDistance;
        audioSource.minDistance = data.minDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();
        Destroy(go, clip.length);
        return audioSource;
    }

}
