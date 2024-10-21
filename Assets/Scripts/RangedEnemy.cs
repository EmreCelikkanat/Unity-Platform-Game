using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [SerializeField] private float attackCd;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireball;

    [SerializeField] private BoxCollider2D box;
    private float cdTimer = Mathf.Infinity;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float colliderDistance;

    private Animator anim;
    private PlayerHealth health;
    private EnemyPatrol enemyPatrol;

    [SerializeField] private AudioClip fireballsound;


    void Start()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cdTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cdTimer > attackCd)
            {
                cdTimer = 0;
                anim.SetTrigger("RangedAttack");
                RangedAttack();
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            health = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
             new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z));
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballsound);
        cdTimer = 0f;
        fireball[FindFireball()].transform.position=firepoint.position;
        fireball[FindFireball()].GetComponent<EnemeyProjectile>().Direction(Mathf.Sign(transform.localScale.x));
        fireball[FindFireball()].GetComponent<EnemeyProjectile>().ActiveProjectiles();
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
            {
                return i;
            }
        }
         return 0;
    }
}
