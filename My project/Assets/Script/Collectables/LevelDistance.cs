using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDistance : MonoBehaviour
{
    public GameObject DistanceDisplay;
    public int distanceCount;
    public bool addDistance = false;
    public float distanceDelay = 0.35f;
    public GameObject DistanceEndDisplay;
    void Update()
    {
        if (addDistance == false)
        {
            addDistance = true;
            StartCoroutine(AddDistance());
        }
    }
    IEnumerator AddDistance()
    {
        distanceCount += 1;
        DistanceDisplay.GetComponent<Text>().text = "" + distanceCount;
        DistanceEndDisplay.GetComponent<Text>().text = "" + distanceCount;
        yield return new WaitForSeconds(distanceDelay);
        addDistance = false;
    }
}
