using System.IO;
using UnityEngine;

public class Storage
{
    public object Load(object saveDataByDefault)
    {
        return saveDataByDefault;
    }

    public void Save(object saveData)
    {
        var jsonDataString = JsonUtility.ToJson(saveData, true);
        var fileStream = new FileStream(
            GetFileName(Random.Range(0, int.MaxValue).ToString()),
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