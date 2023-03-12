using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelFileManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TMP_InputField fileName;
    [SerializeField] private string path;
    private readonly Storage _storage = new();
    
    public void OpenFileExplorer()
    {
        // _path = EditorUtility.OpenFilePanel("Selert level (.save)", Application.persistentDataPath + "/saves/", "save");
        path = Application.persistentDataPath + "/saves/" + fileName.text + ".save";
        StartCoroutine(GetLevel());
    }

    private IEnumerator GetLevel()
    {
        levelManager.lvlData = _storage.Load(path);
        yield return new WaitForSeconds(3);

        if (levelManager.lvlData is not null)
            levelManager.LoadLvl("FromFile");
        else
            Debug.Log("err: file not selected");
    }
}