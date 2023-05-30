using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

   public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}
