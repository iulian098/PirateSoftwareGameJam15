using System.Collections;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;


[DefaultExecutionOrder(-50)]
public class SaveSystem : MonoSingleton<SaveSystem>
{
    public static SaveSystem Instance;
    public static bool stopSaving;

    const string FILE_NAME = "saveFile.data";

    [SerializeField] InventoryContainer vehiclesContainer;

    JsonSerializerSettings jsonSettings = new JsonSerializerSettings() {
        MaxDepth = null,
        CheckAdditionalContent = true
    };
    string filePath;
    SaveFile saveFile;
    SaveFile cloudSaveFile;
    Coroutine saveGameCoroutine;

    public Action OnSaveFileLoaded;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public async Task Init() {
        filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        saveFile = LoadFile();
        LoadGameData();
    }

    IEnumerator SaveGameCoroutine() {
        yield return new WaitForSeconds(5);
        SaveFile();
        saveGameCoroutine = StartCoroutine(SaveGameCoroutine());
    }

     async void LoadGameData() {
        if (saveFile == null) {
            Debug.LogError("[SaveSystem] No save file found, creating new one");
            saveFile = new SaveFile();
        }

        //TODO: Load data

        saveGameCoroutine = StartCoroutine(SaveGameCoroutine());
        OnSaveFileLoaded?.Invoke();

        Debug.Log("[SaveSystem] Save File Loaded");
    } 

    [ContextMenu("ClearSave")]
    void ClearSave() {
        filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        if (File.Exists(filePath))
            File.Delete(filePath);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    SaveFile LoadFile() {
        SaveFile file;

        string data;
        
        if (File.Exists(filePath)) {
            try {
                using (var reader = new StreamReader(filePath)) {
                    data = reader.ReadToEnd();
                    file = JsonConvert.DeserializeObject(data, typeof(SaveFile), jsonSettings) as SaveFile;
                }
                Debug.Log("[SaveSystem] Save file loaded");
            }
            catch (IOException ex) {
                Debug.LogError(ex.Message);
                file = new SaveFile();
                Debug.Log("[SaveSystem] Created new save file");
            }
        }
        else {
            file = new SaveFile();
            Debug.Log("[SaveSystem] Created new save file");
        }
        
        return file;
    }

    void SaveFile() {
        saveFile.saveDate = DateTime.UtcNow;

        if (stopSaving) {
            Debug.Log("[SaveSystem] Save disabled");
            return;
        }

        try {
            using (StreamWriter sw = new StreamWriter(filePath)) {
                string data = JsonConvert.SerializeObject(saveFile, jsonSettings);
                sw.Write(data);
            }
        }catch(IOException ex) {
            Debug.LogError(ex.Message);
        }

        Debug.Log("[SaveSystem] Saved");
    }

}
