using UnityEngine;

[System.Serializable]
public class GameData 
{
    public SerializableDictionary<string, Vector3> objectPosition;
    public GameData(){
        objectPosition = new SerializableDictionary<string, Vector3>();
    }
}
