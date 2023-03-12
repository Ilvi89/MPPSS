using System.IO;
using UnityEngine;

public class Storage
{
    public LevelData LevelData;
    public LevelData Load(string path)
    {
        var fileStream = new StreamReader(path);
        return JsonUtility.FromJson<LevelData>(fileStream.ReadToEnd());
    }

    

    public void Save(LevelData saveData)
    {
        var jsonDataString = JsonUtility.ToJson(saveData, true);
        var fileStream = new FileStream(
            GetFileName(saveData.name),
            FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(jsonDataString);
        }
    }

    private string GetFileName(string name)
    {
        return Application.persistentDataPath + "/saves/" + name + ".save";
    }
}