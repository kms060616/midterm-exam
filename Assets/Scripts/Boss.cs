using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject fishPrefab;
    public Transform firePoint;
    public float hp = 100f;
    public Transform player;

    private bool isDead = false;
    private Vector3 startPos;  // 돌진 시작 위치
    private bool isDashing = false;  // 돌진 중인지 체크
    private float dashDistance = 5f;  // 돌진할 거리

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 플레이어 찾기 (없으면 자동으로 태그로 찾기)
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        StartCoroutine(BossPatternRoutine());

        // HP 감소 Coroutine 시작
        StartCoroutine(DecreaseHP());
    }

    System.Collections.IEnumerator BossPatternRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(2f); // 대기

            int pattern = Random.Range(0, 2);

            if (pattern == 0)
            {
                Debug.Log("돌진 패턴 선택됨");
                Dash();
                yield return new WaitForSeconds(1f); // 돌진하는 시간
                StopDash(); // 돌진 멈추기
            }
            else
            {
                Debug.Log("물고기 패턴 선택됨");
                ShootFish();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void Dash()
    {
        animator.SetTrigger("Dash");

        // 플레이어 쪽 방향 계산
        Vector2 direction = (player.position - transform.position).normalized;

        // 그 방향으로 돌진 (이동)
        rb.velocity = direction * 10f;

        // 보스 이미지 반전 처리 (플레이어의 반대 방향을 바라보도록)
        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)  // 플레이어가 왼쪽에 있을 때
        {
            scale.x = 1;  // 이미지가 왼쪽을 바라보도록 (반대 방향)
        }
        else  // 플레이어가 오른쪽에 있을 때
        {
            scale.x = -1;  // 이미지가 오른쪽을 바라보도록 (반대 방향)
        }

        transform.localScale = scale;
    }

    void Update()
    {
        // 돌진 중이라면
        if (isDashing)
        {
            float distanceTraveled = Vector3.Distance(startPos, transform.position);

            // 설정한 거리 이상 이동했으면 돌진 멈추기
            if (distanceTraveled >= dashDistance)
            {
                StopDash();
            }
        }
    }

    void StopDash()
    {
        rb.velocity = Vector2.zero; // 돌진 멈추기
        isDashing = false;
    }

    void ShootFish()
    {
        animator.SetTrigger("Shoot");

        if (fishPrefab != null && firePoint != null)
        {
            // 보스가 바라보는 방향의 반대 방향 계산
            float direction = transform.localScale.x > 0 ? -1f : 1f;

            // 물고기 생성
            GameObject fish = Instantiate(fishPrefab, firePoint.position, Quaternion.identity);

            // 물고기 움직이기
            Rigidbody2D fishRb = fish.GetComponent<Rigidbody2D>();
            if (fishRb != null)
            {
                fishRb.velocity = new Vector2(direction * 10f, 0); // 반대 방향으로 쏨!
            }
        }
        else
        {
            Debug.LogWarning("fishPrefab 또는 firePoint가 설정되지 않았습니다!");
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
        Destroy(gameObject, 2f); // 2초 뒤 사라짐
    }

    // HP 감소 Coroutine
    System.Collections.IEnumerator DecreaseHP()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);  // 1초마다 대기

            if (hp > 0)
            {
                hp -= 1f;  // HP 1 감소
                Debug.Log("HP 감소: " + hp);  // HP 감소 상태 로그로 확인
            }

            if (hp <= 0)
            {
                Die();  // HP가 0 이하가 되면 죽음 처리
            }
        }
    }
}
