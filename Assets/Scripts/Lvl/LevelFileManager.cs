using System.Collections;
using AnotherFileBrowser.Windows;
using TMPro;
using UnityEngine;

public class LevelFileManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private readonly Storage _storage = new();
    
    
    public void OpenFileExplorer()
    {
        var br = new BrowserProperties
        {
            title = "Save files (*.save)",
            filterIndex = 0
        };

        new FileBrowser().OpenFileBrowser(br, path => { StartCoroutine(GetLevel(path)); });
    }

    private IEnumerator GetLevel(string path)
    {
        levelManager.SetLvlData(_storage.Load(path));
        yield return new WaitForSeconds(3);

        if (levelManager.lvlData is not null)
            levelManager.LoadLvl("FromFile");
        else
            Debug.Log("err: file not selected");
    }
}