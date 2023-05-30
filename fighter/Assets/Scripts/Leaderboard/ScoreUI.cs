using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]private RowUI rowUi;
    [SerializeField]private ScoreManager manager;
    [SerializeField]private Transform content;

    private void Start()
    {
        var playerScore = new BoardScore(PlayerPrefs.GetString("playerName", ""), PlayerPrefs.GetInt("Score"));

        if (PlayerPrefs.GetInt("Score") > 0)
            ScoreManager.AddScore(playerScore);
 
        var scores = manager.GetHighScore().Take(5).ToArray();

        for (var i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
    }

    public void ClearContent()
    {
        for (var i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
    }
}