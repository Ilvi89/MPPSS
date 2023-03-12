using System.Collections;
using UnityEditor;
using UnityEngine;

public class LevelFileManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private string _path;
    private readonly Storage _storage = new();

    public void OpenFileExplorer()
    {
        _path = EditorUtility.OpenFilePanel("Selert level (.save)", Application.persistentDataPath + "/saves/", "save");

        StartCoroutine(GetLevel());
    }

    private IEnumerator GetLevel()
    {
        levelManager.lvlData = _storage.Load(_path);
        yield return new WaitForSeconds(3);

        if (levelManager.lvlData is not null)
            levelManager.LoadLvl("FromFile");
        else
            Debug.Log("err: file not selected");
    }
}