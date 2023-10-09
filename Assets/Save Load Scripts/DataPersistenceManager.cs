using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;  
    private FileDataHandler dataHandler;
    private AWSBucketBehavior awsBucketBehavior;


    public void NewGame(){
        gameData = new GameData();
    }

    private void Start(){
        //https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.awsBucketBehavior = new AWSBucketBehavior(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();


        //Enable this to load on start
        // LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void SaveGame(){
        //If there's no game saved initialize to a new game
        if(this.gameData == null){
            Debug.Log("Initializing to default values");
            NewGame();
        }
        
        //Pass data to all the scripts that need it
        foreach(IDataPersistence dataPersistenceObject in dataPersistenceObjects){
            dataPersistenceObject.SaveData(ref gameData);
        }
        
        //Save data to a file using data handler 
        dataHandler.Save(this.gameData);
        awsBucketBehavior.UploadFileToAWS3();
    }

    public void LoadGame(){
        //Load any saved game from a file using data handler
        this.gameData = dataHandler.Load();

        //If there's no game saved initialize to a new game
        if(this.gameData == null){
            Debug.Log("Initializing to default values");
            NewGame();
        }

        //Push all the loaded data to other scripts that need it 
        foreach(IDataPersistence dataPersistenceObject in dataPersistenceObjects){
            dataPersistenceObject.LoadData(gameData);
        }
    }

    public void OnApplicationQuit(){
        //Enable this to save on quit
        // SaveGame();
    }

}
