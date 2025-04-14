using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerTwo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;

    
    private bool isInvincible = false;
    private float invincibleTime = 0f;

    
    private float speedBoostMultiplier = 1f;
    private float speedBoostTime = 0f;

    
    private float jumpForceMultiplier = 1f;
    private float jumpBoostTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0f)
            {
                isInvincible = false;
            }
        }

        
        if (speedBoostTime > 0f)
        {
            speedBoostTime -= Time.deltaTime;
            if (speedBoostTime <= 0f)
            {
                speedBoostMultiplier = 1f;
            }
        }

        
        if (jumpBoostTime > 0f)
        {
            jumpBoostTime -= Time.deltaTime;
            if (jumpBoostTime <= 0f)
            {
                jumpForceMultiplier = 1f;
            }
        }

        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed * speedBoostMultiplier, rb.velocity.y);

        
        if (moveInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        
        pAni.SetFloat("Speed", Mathf.Abs(moveInput));

        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce * jumpForceMultiplier, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }
    }

    
    public void ActivateInvincibility(float duration)
    {
        isInvincible = true;
        invincibleTime = duration;
    }

    
    public void ActivateSpeedBoost(float duration, float speedMultiplier)
    {
        speedBoostMultiplier = speedMultiplier;
        speedBoostTime = duration;
    }

    
    public void ActivateJumpBoost(float duration, float multiplier)
    {
        jumpForceMultiplier = multiplier;
        jumpBoostTime = duration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") && !isInvincible)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}