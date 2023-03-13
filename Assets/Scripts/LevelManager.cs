using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // TODO: enum
    [SerializeField] public LvlMode lvlMode;
    public LevelData lvlData;
    public string lvlDataName;
    [SerializeField] public List<ShipData> ListOfShipData;

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

    public void SetLvlData(LevelData data)
    {
        Instance.lvlDataName = data.name;
        Instance.lvlData = data;
    }

    public ShipData GetShipData(int i)
    {
        return ListOfShipData[i];
    }


    public void LoadLvl(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetMode(int mode)
    {
        lvlMode = (LvlMode) Enum.ToObject(typeof(LvlMode), mode);
    }
}

public enum LvlMode
{
    Day = 0,
    Night = 1,
    Fog = 2
}