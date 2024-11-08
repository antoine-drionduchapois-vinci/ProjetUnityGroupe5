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
    

    
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(this.gameObject.transform.position.x > LevelBoundery.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
          
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(this.gameObject.transform.position.x < LevelBoundery.rightSide) {
                transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
            }
           
        
        }
    }
}
