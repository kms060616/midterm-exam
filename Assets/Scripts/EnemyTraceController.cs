using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .5f;
    public float raycastDistance = 0.2f;
    public float tranceDistance = 2f;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude > tranceDistance)
            return;

        Vector2 directionNormalized = direction.normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternativeDiretion = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDiretion * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}
