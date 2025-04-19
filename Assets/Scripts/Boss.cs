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
    private Vector3 startPos;  // ���� ���� ��ġ
    private bool isDashing = false;  // ���� ������ üũ
    private float dashDistance = 5f;  // ������ �Ÿ�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �÷��̾� ã�� (������ �ڵ����� �±׷� ã��)
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        StartCoroutine(BossPatternRoutine());

        // HP ���� Coroutine ����
        StartCoroutine(DecreaseHP());
    }

    System.Collections.IEnumerator BossPatternRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(2f); // ���

            int pattern = Random.Range(0, 2);

            if (pattern == 0)
            {
                Debug.Log("���� ���� ���õ�");
                Dash();
                yield return new WaitForSeconds(1f); // �����ϴ� �ð�
                StopDash(); // ���� ���߱�
            }
            else
            {
                Debug.Log("����� ���� ���õ�");
                ShootFish();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void Dash()
    {
        animator.SetTrigger("Dash");

        // �÷��̾� �� ���� ���
        Vector2 direction = (player.position - transform.position).normalized;

        // �� �������� ���� (�̵�)
        rb.velocity = direction * 10f;

        // ���� �̹��� ���� ó�� (�÷��̾��� �ݴ� ������ �ٶ󺸵���)
        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)  // �÷��̾ ���ʿ� ���� ��
        {
            scale.x = 1;  // �̹����� ������ �ٶ󺸵��� (�ݴ� ����)
        }
        else  // �÷��̾ �����ʿ� ���� ��
        {
            scale.x = -1;  // �̹����� �������� �ٶ󺸵��� (�ݴ� ����)
        }

        transform.localScale = scale;
    }

    void Update()
    {
        // ���� ���̶��
        if (isDashing)
        {
            float distanceTraveled = Vector3.Distance(startPos, transform.position);

            // ������ �Ÿ� �̻� �̵������� ���� ���߱�
            if (distanceTraveled >= dashDistance)
            {
                StopDash();
            }
        }
    }

    void StopDash()
    {
        rb.velocity = Vector2.zero; // ���� ���߱�
        isDashing = false;
    }

    void ShootFish()
    {
        animator.SetTrigger("Shoot");

        if (fishPrefab != null && firePoint != null)
        {
            // ������ �ٶ󺸴� ������ �ݴ� ���� ���
            float direction = transform.localScale.x > 0 ? -1f : 1f;

            // ����� ����
            GameObject fish = Instantiate(fishPrefab, firePoint.position, Quaternion.identity);

            // ����� �����̱�
            Rigidbody2D fishRb = fish.GetComponent<Rigidbody2D>();
            if (fishRb != null)
            {
                fishRb.velocity = new Vector2(direction * 10f, 0); // �ݴ� �������� ��!
            }
        }
        else
        {
            Debug.LogWarning("fishPrefab �Ǵ� firePoint�� �������� �ʾҽ��ϴ�!");
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
        Destroy(gameObject, 2f); // 2�� �� �����
    }

    // HP ���� Coroutine
    System.Collections.IEnumerator DecreaseHP()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);  // 1�ʸ��� ���

            if (hp > 0)
            {
                hp -= 1f;  // HP 1 ����
                Debug.Log("HP ����: " + hp);  // HP ���� ���� �α׷� Ȯ��
            }

            if (hp <= 0)
            {
                Die();  // HP�� 0 ���ϰ� �Ǹ� ���� ó��
            }
        }
    }
}
