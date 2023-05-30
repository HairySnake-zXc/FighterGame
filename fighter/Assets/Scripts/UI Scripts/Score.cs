using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static int _score;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = $"Score: {_score}";
        PlayerPrefs.SetInt("Score", _score);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScore(1);
        }
    }

    private static void ChangeScore(int value) => _score += value;

    private void OnDestroy()
    {
        _score = 0;
    }
}
