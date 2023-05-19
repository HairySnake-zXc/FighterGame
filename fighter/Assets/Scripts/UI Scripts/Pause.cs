using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pause : MonoBehaviour
{
    [SerializeField] private Behaviour canvas;
    private bool _gameIsPaused;

    private void Start()
    {
        canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            _gameIsPaused = !_gameIsPaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if(_gameIsPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1;
    }
}
