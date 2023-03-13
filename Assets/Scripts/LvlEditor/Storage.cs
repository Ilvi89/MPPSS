using System.IO;
using AnotherFileBrowser.Windows;
using UnityEngine;

public class Storage
{
    public LevelData LevelData;

    public LevelData Load(string path)
    {
        var fileStream = new StreamReader(path);
        var json = JsonUtility.FromJson<LevelData>(fileStream.ReadToEnd());
        
        return json;
    }


    public void Save(LevelData saveData)
    {
        var bp = new BrowserProperties();
        bp.filter = "save files (*.save)|*.save";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, saveData.name, ".save", path =>
        {
            var jsonDataString = JsonUtility.ToJson(saveData, true);
            var fileStream = new FileStream(path,
                FileMode.Create);

            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(jsonDataString);
            }
        });
    }
}