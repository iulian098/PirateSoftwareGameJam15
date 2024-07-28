using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsContainer", menuName = "Scriptable Objects/SoundsContainer")] 
public class SoundsContainer : ScriptableObject
{
    [SerializeField] SoundData[] soundEffects;

    Dictionary<string, SoundData> soundEffectsDict = new Dictionary<string, SoundData>();

    private void OnEnable() {
        GenerateDictionary();
    }

    private void GenerateDictionary() {
        foreach (var item in soundEffects) {
            soundEffectsDict.Add(item.name, item);
        }
    }

    public SoundData GetSound(string soundName) {
        if (soundEffectsDict.Count == 0)
            GenerateDictionary();
        if(soundEffectsDict.TryGetValue(soundName, out var result))
            return result;
        /*SoundData sound = soundEffects.First(x => x.name == soundName);
        if (sound == null)
            Debug.Log("No sound with name " + soundName + " found");*/
        return null;
    }
}
