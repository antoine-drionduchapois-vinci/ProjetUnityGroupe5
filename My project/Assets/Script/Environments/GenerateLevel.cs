using System.Collections;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section; // Array of section prefabs
    public int zPos = 30; // Z position for the next section
    public bool creatingSection = false;
    public int secNumb;

    public float initialWaitTime = 2f; // Starting generation wait time
    public float minWaitTime = 0.4f; // Minimum generation wait time
    public float speedIncreaseRate = 0.4f; // Rate at which player speed increases per second
    private float currentWaitTime;

    void Start()
    {
        // Set the starting wait time
        currentWaitTime = initialWaitTime;
    }

    void Update()
    {
        if (!creatingSection)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }

        // Gradually decrease the wait time based on speedIncreaseRate
        currentWaitTime = Mathf.Max(minWaitTime, currentWaitTime - (speedIncreaseRate * Time.deltaTime));
    }

    IEnumerator GenerateSection()
    {
        // Generate a random section
        secNumb = Random.Range(0, section.Length);
        Instantiate(section[secNumb], new Vector3(0, 0, zPos), Quaternion.identity);

        // Increment the zPos for the next section
        zPos += 30;

        // Wait before generating the next section
      
        yield return new WaitForSeconds(currentWaitTime);

        creatingSection = false;
    }
}
