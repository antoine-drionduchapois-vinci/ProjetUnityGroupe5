using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRunSequence : MonoBehaviour
{
    public GameObject LiveCoins;
    public GameObject LiveDistance;
    public GameObject EndScreen;
    public GameObject FadeOut;
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene(0);
    }
}
