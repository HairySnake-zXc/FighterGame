using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Update()
    {
        scoreText.text = $"Score: {_score}";
    }

    private void ChangeScore(int value) => _score += value;
}
