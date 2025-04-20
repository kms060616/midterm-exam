using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Boss : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject fishPrefab;
    public Transform firePoint;
    public float hp = 100f;
    public Transform player;

    private bool isDead = false;
    private Vector3 startPos;  
    private bool isDashing = false;  
    private float dashDistance = 5f;  

    public GameObject goalPrefab;  
    public Transform goalSpawnPoint;

    public TextMeshProUGUI bossTimerText;
    private float bossTime;
    public GameObject specialFishPrefab;

    void Start()
    {
        bossTime = hp;
        StartCoroutine(BossTimerRoutine());

        System.Collections.IEnumerator BossTimerRoutine()
        {
            while (!isDead && bossTime > 0)
            {
                yield return new WaitForSeconds(1f);
                bossTime -= 1f;
                hp -= 1f;

                if (bossTimerText != null)
                {
                    bossTimerText.text = $"보스 체력: {bossTime}";
                }

                if (hp <= 0)
                {
                    Die();
                }
            }
        }

        rb = GetComponent<Rigidbody2D>();

        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        StartCoroutine(BossPatternRoutine());

        
        StartCoroutine(DecreaseHP());
    }

    System.Collections.IEnumerator BossPatternRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(2f); 

            int pattern = Random.Range(0, 2);

            if (pattern == 0)
            {
                
                Dash();
                yield return new WaitForSeconds(1f); 
                StopDash(); 
            }
            else
            {
                
                ShootFish();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void Dash()
    {
        animator.SetTrigger("Dash");

        
        Vector2 direction = (player.position - transform.position).normalized;

        
        rb.velocity = direction * 20f;

        
        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)  
        {
            scale.x = 1;  
        }
        else  
        {
            scale.x = -1;  
        }

        transform.localScale = scale;
    }

    void Update()
    {
        if (isDead || player == null) return;

        
        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); 
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x); 
        }
        transform.localScale = scale;

        if (isDashing)
        {
            float distanceTraveled = Vector3.Distance(startPos, transform.position);

            
            if (distanceTraveled >= dashDistance)
            {
                StopDash();
            }
        }
    }

    void StopDash()
    {
        rb.velocity = Vector2.zero; 
        isDashing = false;
    }

    void ShootFish()
    {
        animator.SetTrigger("Shoot");

        GameObject prefabToSpawn;

        
        if (Random.value < 0.8f)
        {
            prefabToSpawn = fishPrefab;
            
        }
        else
        {
            prefabToSpawn = specialFishPrefab;
            
        }

        if (prefabToSpawn != null)
        {
            
            Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;
            GameObject fish = Instantiate(prefabToSpawn, firePoint.position, Quaternion.identity);

            Rigidbody2D fishRb = fish.GetComponent<Rigidbody2D>();
            if (fishRb != null)
            {
                fishRb.velocity = direction * 20f;
            }
        }
        
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        rb.velocity = Vector2.zero;

        
        if (goalPrefab != null)
        {
            Vector3 spawnPosition = goalSpawnPoint != null ? goalSpawnPoint.position : transform.position;
            Instantiate(goalPrefab, spawnPosition, Quaternion.identity);
        }
        

        Destroy(gameObject, 2f); 
    }

    
    System.Collections.IEnumerator DecreaseHP()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);  

            

            if (hp <= 0)
            {
                Die();  
            }
        }
    }
}
