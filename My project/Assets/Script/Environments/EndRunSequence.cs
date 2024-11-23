using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndRunSequence : MonoBehaviour
{
    public GameObject LiveCoins;
    public GameObject LiveDistance;
    public GameObject EndScreen;
    public GameObject FadeOut;

    private int finalDistanceValue;
    private int finalCoinsValue;
    // Start is called before the first frame update
    void Start()
    {
        if (LiveDistance == null || LiveCoins == null)
        {
            Debug.LogError("LiveDistance ou LiveCoins n'est pas assigné dans l'Inspecteur !");
            return;
        }
        Text finalDistanceText = LiveDistance.GetComponent<Text>();
        Text finalCoinsText = LiveCoins.GetComponent<Text>();

        if (int.TryParse(finalDistanceText.text, out finalDistanceValue))
        {
            Debug.Log("Distance finale convertie avec succès : " + finalDistanceValue);
        }
        else
        {
            Debug.LogError("Erreur : Impossible de convertir la distance en entier.");
        }

        if (int.TryParse(finalCoinsText.text, out finalCoinsValue))
        {
            Debug.Log("Total des pièces converti avec succès : " + finalCoinsValue);
        }
        else
        {
            Debug.LogError("Erreur : Impossible de convertir les pièces en entier.");
        }

        StartCoroutine(EndRun());
    }

    IEnumerator EndRun()
    {
        yield return new WaitForSeconds(3);
        LiveCoins.SetActive(false);
        LiveDistance.SetActive(false);
        EndScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(2);

        SaveScore();
        SceneManager.LoadScene(0);
    }

    void SaveScore()
    {
        Debug.Log("Scores actuels dans PlayerPrefs : " + PlayerPrefs.GetString("TopScores", "Aucun score sauvegardé"));


        Leaderboard leaderboard = FindObjectOfType<Leaderboard>();
        if (leaderboard != null)
        {
            leaderboard.SaveLocalScore(finalDistanceValue);
        }
    }
}
