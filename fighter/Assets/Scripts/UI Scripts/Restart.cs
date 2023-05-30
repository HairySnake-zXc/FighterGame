using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartLevel()
    {
        var playerScore = new BoardScore(PlayerPrefs.GetString("playerName", ""), PlayerPrefs.GetInt("Score"));

        if (PlayerPrefs.GetInt("Score") > 0)
            ScoreManager.AddScore(playerScore);
        
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}
