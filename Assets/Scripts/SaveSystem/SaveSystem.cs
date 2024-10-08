using System.Collections;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;


[DefaultExecutionOrder(-50)]
public class SaveSystem : MonoSingleton<SaveSystem>
{
    public static bool stopSaving;

    const string FILE_NAME = "saveFile.data";

    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] RecipesContainer recipesContainer;
    [SerializeField] GameObject continueButtonObj;

    JsonSerializerSettings jsonSettings = new JsonSerializerSettings() {
        MaxDepth = null,
        CheckAdditionalContent = true
    };
    string filePath;
    SaveFile saveFile;
    Coroutine saveGameCoroutine;

    public Action OnSaveFileLoaded;
    public Action OnNewGameBegin;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start() {
        Init();
    }

    public void NewGame() {
        if (File.Exists(filePath))
            CommonPopup.Instance.Show("Are you sure you want to start a new game?", BeginNewGame, "Yes", true, cancelButtonText: "No");
        else
            BeginNewGame();
    }

    void BeginNewGame() {
        saveFile = new SaveFile();
        LoadGameData();
        saveGameCoroutine = StartCoroutine(SaveGameCoroutine());
        OnNewGameBegin?.Invoke();
    }

    public void ContinueGame() {
        saveFile = LoadFile();

        LoadGameData();
    }

    public void Init() {
        filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        continueButtonObj.SetActive(File.Exists(filePath));
    }

    IEnumerator SaveGameCoroutine() {
        yield return new WaitForSeconds(5);
        SaveFile();
        saveGameCoroutine = StartCoroutine(SaveGameCoroutine());
    }

     void LoadGameData() {
        if (saveFile == null) {
            Debug.LogError("[SaveSystem] No save file found, creating new one");
            saveFile = new SaveFile();
        }

        //TODO: Load data
        UserManager.SetPlayerData(saveFile.playerData);
        inventoryContainer.SetSaveData(saveFile.inventoryItemsIds, saveFile.inventoryItemsAmounts, saveFile.hotbarItems, saveFile.hotbarSelectedSlot);
        inventoryContainer.ConsumableID = saveFile.consumableSlotItemID;
        recipesContainer.SetSaveData(saveFile.unlockedRecipes);

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
