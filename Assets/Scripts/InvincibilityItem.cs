using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : MonoBehaviour
{
    public float invincibleDuration = 5f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerTwo player = other.GetComponent<PlayerControllerTwo>();
        if (player != null)
        {
            player.ActivateInvincibility(invincibleDuration);  
            Destroy(gameObject);  
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
