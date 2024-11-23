using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingUI : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;

    void Start()
    {
        ShowLocalScores();
    }

    public void ShowLocalScores()
    {
        Leaderboard leaderboard = FindObjectOfType<Leaderboard>();
        if (leaderboard != null)
        {
            List<int> scores = leaderboard.GetLocalScores();

            Debug.Log("Scores chargés dans RankingUI : " + string.Join(", ", scores));

            first.GetComponent<Text>().text = scores.Count > 0 ? scores[0].ToString() : "N/A";
            second.GetComponent<Text>().text = scores.Count > 1 ? scores[1].ToString() : "N/A";
            third.GetComponent<Text>().text = scores.Count > 2 ? scores[2].ToString() : "N/A";


        }
        else
        {
            Debug.LogError("Leaderboard introuvable dans la scène !");
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
