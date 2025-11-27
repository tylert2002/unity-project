using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System; 
using System.IO;

public class DataPersistence : MonoBehaviour
{
    [Header("File Storage Config")]


    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistence instance { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the Scene");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        
    }

    /*private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }*/

   /* private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }*/

    /*public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame(); 
    }

    public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnloaded called");
        SaveGame();     
    }*/

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // you had += here
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
       
    }

    /*private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }*/


    public void NewGame()
    {
        this.gameData = new GameData();
    }


    public bool LoadGame()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        this.gameData = dataHandler.Load(); 

        if (this.gameData == null) 
        { 
            Debug.Log("No data was found. A new game needs to be loaded before data can be saved."); 
            NewGame();
        } 
        
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        { 
            dataPersistenceObj.LoadData(gameData); 
        } 
        
        Debug.Log("Loaded health = " + gameData.health);
        return true;


    }


    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game nees to be started before data can be saved.");
            NewGame();
        }
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }


        dataHandler.Save(gameData);
    }


    public void OnSaveButton()
    {
        SaveGame();
    }


    public void OnLoadButton()
    {
        LoadGame();
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();


        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

}