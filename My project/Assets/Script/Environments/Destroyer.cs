using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string parentName;
    public GameObject player;

    void Start()
    {
       player = GameObject.Find("Player");
        parentName = transform.name;
        StartCoroutine(DestroyClone());
    }

    IEnumerator DestroyClone()
    {
        // Repeat deletion every `interval` seconds
        while (true)
        {
            yield return new WaitForSeconds(40);
           
            if (player.transform.position.z - gameObject.transform.position.z > 200)
            {
                if (parentName.Contains("SectionE(Clone)"))
                    yield return new WaitForSeconds(120);
                Destroy(gameObject);
                yield break; // Exit the loop once this object is destroyed
            }
        }
    }
}
