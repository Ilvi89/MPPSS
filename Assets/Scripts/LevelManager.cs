using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance; 

    // TODO: enum
    [SerializeField] public LvlMode lvlMode;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void LoadLvl(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetMode(int mode)
    {
        lvlMode = (LvlMode)Enum.ToObject(typeof(LvlMode), mode);
    }
}

public enum LvlMode
{
    Day = 0,
    Night = 1,
    Fog = 2
}