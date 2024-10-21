using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private Transform walkPointA;
    [SerializeField] private Transform walkPointB;
    public Transform currentPoint;

    [SerializeField] private float walkSpeed = 2f;

    public Rigidbody2D rb;
    void Start()
    {
        currentPoint = walkPointB;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 point = currentPoint.position-transform.position;
        if(currentPoint == walkPointB.transform )
        {
            rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
        }
        
        else
        {
            rb.velocity= new Vector2(walkSpeed, rb.velocity.y);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint==walkPointB.transform)
        {
            Flip();
            currentPoint = walkPointA.transform;
        }
        else if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == walkPointA.transform)
        {
            Flip();
            currentPoint = walkPointB.transform;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(walkPointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(walkPointB.transform.position, 0.5f);
        Gizmos.DrawLine(walkPointA.transform.position, walkPointB.transform.position);
    }
    private void Flip()
    {
        Vector3 localScalae = transform.localScale;
        localScalae.x*=-1;
        transform.localScale = localScalae;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
