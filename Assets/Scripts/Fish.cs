using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {

        if(!CompareTag("SpecialFish") && collision.gameObject.CompareTag("Ground"))
    {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
