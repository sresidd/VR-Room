using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "VR";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(){
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadData = null;

        if(File.Exists(fullPath)){
            try
            {
                //load the serialized data form the file
                string dataToLoad = "";

                using ( FileStream stream = new FileStream(fullPath, FileMode.Open)){
                    using( StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //optionally decrypt the data 
                if(useEncryption) dataToLoad = EncryptDecrypt(dataToLoad);

                //deserialize the loaded data form the file to c# objects
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred while trying to load form the file: " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Save(GameData data){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try{
            //create the directory path if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize game data object to a json
            string dataToStore = JsonUtility.ToJson(data, true);

            //optionally encrypt the data 
            if(useEncryption) dataToStore = EncryptDecrypt(dataToStore);

            //write the serialized data to the file 
            using( FileStream stream = new FileStream(fullPath, FileMode.Create)){
                using ( StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e){
            Debug.LogError("Error occurred while trying to save to the file: " + fullPath + "\n" + e);
        }
    }

    //simple implementation of XOR encryption
    private string EncryptDecrypt(string data){
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i%encryptionCodeWord.Length]);
        {

        return modifiedData;
        }
    }
}
