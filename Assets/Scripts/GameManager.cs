using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    private static bool _gameIsPaused;
    private static bool _gameIsSlowed;

    public void PauseGame()
    {
        _gameIsPaused = !_gameIsPaused;
        if (_gameIsPaused)
        {
            _gameIsSlowed = false;
            Time.timeScale = 0f;
        }
        else
        {
            _gameIsSlowed = false;
            Time.timeScale = 1;
        }
    }


    public void SlowGame()
    {
        _gameIsSlowed = !_gameIsSlowed;
        if (_gameIsSlowed)
        {
            _gameIsPaused = false;
            Time.timeScale = 0.5f;
        }
        else
        {
            _gameIsPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void EndGame()
    {
        PauseGame();
        ShowEndPanel();
    }

    private void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}