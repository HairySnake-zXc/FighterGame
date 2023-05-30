using System;
using UnityEngine;

[Serializable]
public class BoardScore
{
    public string name;
    public int score;

    public BoardScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
