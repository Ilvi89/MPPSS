using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}