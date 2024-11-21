using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sectionOutside; // Array of section prefabs
    public GameObject[] sectioninside;
    public int zPos = 30; // Z position for the next section
    public bool creatingSection = false;
    public int secNumb;
    public int tailleDeLaZoneHorsDuTemple =50;
    public LevelDistance distancePlayed;
    public float initialWaitTime = 4f; // Starting generation wait time
    public float minWaitTime = 1f; // Minimum generation wait time
    public float speedIncreaseRate = 0.4f; // Rate at which player speed increases per second
    private float currentWaitTime;
    public GameObject Canion;
    public int distance;
    public GameObject Temple;
    bool intersection = false;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
            // Set the starting wait time
            currentWaitTime = initialWaitTime;
    }

    void Update()
    {
 

        if (!creatingSection)
        {
            if (intersection) { StartCoroutine(waitForCanion()); }
            else
            {
                creatingSection = true;
                StartCoroutine(GenerateSection());
            }
        }

        // Gradually decrease the wait time based on speedIncreaseRate
        currentWaitTime = Mathf.Max(minWaitTime, currentWaitTime - (speedIncreaseRate * Time.deltaTime));
    }
    IEnumerator waitForCanion()
    {
        distance = distancePlayed.distanceCount;
        if (distance- tailleDeLaZoneHorsDuTemple > 100)
        {
            intersection=false;
        }
            yield return new WaitForSeconds(currentWaitTime);
    }

    IEnumerator GenerateSection()
    {
        Debug.Log(zPos -(int) player.transform.position.z);
        if ((zPos - (int)player.transform.position.z<1000))
        {
            distance = distancePlayed.distanceCount;
            if (distance > tailleDeLaZoneHorsDuTemple+30)
            {
                // Generate a random section
                secNumb = Random.Range(0, sectioninside.Length);
                GameObject g = Instantiate(sectioninside[secNumb], new Vector3(35, 0, zPos), sectioninside[secNumb].transform.rotation);
                g.GetComponent<Destroyer>().enabled = true;
                // Increment the zPos for the next section
                zPos += 187;

                // Wait before generating the next section
            }


            else if (distance < tailleDeLaZoneHorsDuTemple)
            {
                // Generate a random section
                secNumb = Random.Range(0, sectionOutside.Length);
                GameObject g = Instantiate(sectionOutside[secNumb], new Vector3(0, 0, zPos), sectionOutside[secNumb].transform.rotation);
                g.GetComponent<Destroyer>().enabled = true;
                // Increment the zPos for the next section
                zPos += 30;
                secNumb = Random.Range(0, sectionOutside.Length);
                g = Instantiate(sectionOutside[secNumb], new Vector3(0, 0, zPos), sectionOutside[secNumb].transform.rotation);
                g.GetComponent<Destroyer>().enabled = true;
                // Increment the zPos for the next section
                zPos += 30;

                // Wait before generating the next section
            }
            else
            {
                // instanciate the canion
                zPos += 200;
                GameObject g = Instantiate(Canion, new Vector3(+20.70001f, -1, zPos), Quaternion.identity);
                g.GetComponent<Destroyer>().enabled = true;
                // Increment the zPos for the next section
                zPos += 400;
                g = Instantiate(Temple, new Vector3(0, 0, zPos), Quaternion.identity);
                g.GetComponent<Destroyer>().enabled = true;
                zPos += 280;
                intersection = true;
                // Wait before generating the next section
            }
        }
        yield return new WaitForSeconds(currentWaitTime);

        creatingSection = false;
    }
}