using TMPro;
using UnityEngine;

public class NameInputUI : MonoBehaviour {
    
    public TMP_InputField inputField;
    
    private void Start () {
        inputField.ActivateInputField();
    }
    
    public void StartGame () {
        if (inputField.text.Length > 0) 
        {
            PlayerPrefs.SetString("playerName", inputField.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
            Time.timeScale = 1;
        }
    }
}
