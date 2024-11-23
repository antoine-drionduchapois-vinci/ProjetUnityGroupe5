using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string TopScoresKey = "TopScores";

    public void SaveLocalScore(int distance)
    {

        List<int> scores = GetLocalScores();

        scores.Add(distance);

        Debug.Log("Scores avant tri : " + string.Join(", ", scores));

        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > 3)
        {
            scores = scores.GetRange(0, 3);
        }
        Debug.Log("Scores après tri : " + string.Join(", ", scores));

        string jsonScores = JsonUtility.ToJson(new ScoreList { Scores = scores });
        PlayerPrefs.SetString(TopScoresKey, jsonScores);
        PlayerPrefs.Save();
    }

    // Récupère les scores locaux
    public List<int> GetLocalScores()
    {
        string jsonScores = PlayerPrefs.GetString(TopScoresKey, "");

        if (!string.IsNullOrEmpty(jsonScores))
        {
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(jsonScores);
            return scoreList.Scores;
        }

        // Retourne une liste vide s'il n'y a pas encore de scores
        return new List<int>();
    }
}

