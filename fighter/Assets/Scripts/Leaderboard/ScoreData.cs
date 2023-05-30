using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData
{
    public List<BoardScore> scores;

    public ScoreData()
    {
        scores = new List<BoardScore>();
    }
}
