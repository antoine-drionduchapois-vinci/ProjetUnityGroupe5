using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    // Start is called before the first frame update
    public float leftRightSpeed = 4;
    // Update is called once per frame
   
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);



        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Allow movement to the boundary, not just before it
            Debug.Log("Left key pressed");
       
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
           
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Allow movement to the boundary, not just before it
          
                transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
           
        }

    }
}
