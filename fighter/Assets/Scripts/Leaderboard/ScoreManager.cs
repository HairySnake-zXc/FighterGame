using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreData data;

    private void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        data = JsonUtility.FromJson<ScoreData>(json);
    }

    public IEnumerable<BoardScore> GetHighScore() => data.scores.OrderByDescending(x => x.score);

    public static void AddScore(BoardScore score) => data.scores.Add(score);
    
    public void ClearScore() => data.scores.Clear();

    private void OnDestroy()
    {
        SaveScore();
    }

    private void SaveScore()
    {
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("scores", json);
    }
}
