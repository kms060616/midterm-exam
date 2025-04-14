using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    
    public float boostDuration = 5f;  
    public float speedMultiplier = 2f; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerTwo player = other.GetComponent<PlayerControllerTwo>();
        if (player != null)
        {
            player.ActivateSpeedBoost(boostDuration, speedMultiplier); 
            Destroy(gameObject);
        }
    }

}
