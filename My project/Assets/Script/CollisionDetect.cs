using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    {SerializeField} GameObject thePlayer;

    void OnTriggerEnter(Collider other)
    {
        thePlayer.GetComponent<PlayerMov>
    }
}
