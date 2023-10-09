using UnityEngine;

public class PositionObject : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateID() => id = System.Guid.NewGuid().ToString();
    private Vector3 position;
    public void LoadData(GameData data)
    {
        data.objectPosition.TryGetValue(id, out position);
        transform.position = position;
    }

    public void SaveData(ref GameData data)
    {
        if(data.objectPosition.ContainsKey(id)){
            data.objectPosition.Remove(id);
        }
        data.objectPosition.Add(id, transform.position);
    }

}
