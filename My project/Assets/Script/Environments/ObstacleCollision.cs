using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thePlayer;
    public GameObject charModel;
    public GameObject LevelControl;
    void OnTriggerEnter(Collider other){

        
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        thePlayer.GetComponent<PlayerMove>().enabled = false;
        Debug.Log("Collision detected");
        charModel.GetComponent<Animator>().Play("Stumble Backwards");

        LevelControl.GetComponent<LevelDistance>().enabled = false;
        LevelControl.GetComponent<EndRunSequence>().enabled = true;


    }
}
