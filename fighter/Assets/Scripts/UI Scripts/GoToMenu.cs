using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public void GoMenu() => SceneManager.LoadScene("Menu");
}
