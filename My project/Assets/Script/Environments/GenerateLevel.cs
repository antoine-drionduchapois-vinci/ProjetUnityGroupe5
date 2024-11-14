using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] section;
    public int zPos = 30;
    public bool creatingSection = false;
    public int secNumb;

    
    // Update is called once per frame
    void Update()
    {
        if(creatingSection == false){
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }


    IEnumerator GenerateSection()
    {
        // Pour l'instant il y a 3 sections donc 3 mais
        // si rajoute des sections il faut changer le nombre
        secNumb = Random.Range(0, 3);
        Instantiate(section[secNumb], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 30;
        //A modifier en fonction de la vitesse du jeux
        yield return new WaitForSeconds(3);
        creatingSection= false;
    }
}
