using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemove : MonoBehaviour
{
    public GameObject circle;
    public Vector3 ctarget;
    public GameObject square;
    public Vector3 starget;
    public float speed = 5;

    
    void Update()
    {   
        //circle.transform.position = Vector3.MoveTowards(circle.transform.position, new Vector3 ( 1,1,1), speed);
        if ( Input.GetKeyDown(KeyCode.V))
        {
            circle.transform.position = new Vector2 (circle.transform.position.x + 2f, circle.transform.position.y);
        }
        
    }
}
