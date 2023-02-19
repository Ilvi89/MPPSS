using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance; 

    // TODO: enum
    [SerializeField] public int lvlMode;
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
        lvlMode = sceneName == "day" ? 0 : 1;
        SceneManager.LoadScene("lvl1");
    }
}