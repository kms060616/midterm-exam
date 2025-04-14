using UnityEngine;

public class JumpBoostItem : MonoBehaviour
{
    public float boostDuration = 5f;    
    public float jumpMultiplier = 1.5f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerTwo player = other.GetComponent<PlayerControllerTwo>();
        if (player != null)
        {
            player.ActivateJumpBoost(boostDuration, jumpMultiplier);
            Destroy(gameObject);
        }
    }
}