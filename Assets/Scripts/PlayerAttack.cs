using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCd=1f;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cdTimer=0f;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private AudioClip fireballSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && cdTimer> attackCd && playerMovement.CanAttack()) 
        {
            Attack();
            
        }
        cdTimer += Time.deltaTime;
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cdTimer = 0f;

        fireballs[FindFireball()].transform.position=firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        fireballs[FindFireball()].GetComponent<Projectile>().ActivateProjectiles();

    }
    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
