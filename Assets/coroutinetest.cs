using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class coroutinetest : MonoBehaviour
{
    IEnumerator test()
    {
        int x = 5;
        while (true)
        {
            Debug.Log(x);
            yield return new WaitForSeconds(1);
            x--;
            
            if (x<0) yield break;
        }  
        
    }
    void Start()
    {
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
