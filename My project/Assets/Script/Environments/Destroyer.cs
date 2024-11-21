using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string parentName;
   

    void Start()
    {
        parentName = transform.name;
        StartCoroutine(DestroyClone());
    }

    IEnumerator DestroyClone()
    {
        // Repeat deletion every `interval` seconds
        while (true)
        {
            yield return new WaitForSeconds(40);

            // Check for the correct name or use a more flexible condition as needed
            if (parentName.Contains("Section(Clone)"))
            {
                Destroy(gameObject);
                yield break; // Exit the loop once this object is destroyed
            }
        }
    }
}
