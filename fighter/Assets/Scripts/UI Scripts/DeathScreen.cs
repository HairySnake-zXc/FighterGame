using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Canvas screen;
    private bool _gameIsPaused;

    private void Start()
    {
        screen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameIsPaused = !_gameIsPaused;
            PauseGame();
        }
    }

    private void PauseGame ()
    {
        if(_gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}
