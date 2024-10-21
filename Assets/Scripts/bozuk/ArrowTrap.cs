using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown=1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Arrows;

    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        Arrows[FindArrow()].transform.position=firePoint.position;
        Arrows[FindArrow()].GetComponent<EnemeyProjectile>().Direction(Mathf.Sign(transform.localScale.x));
        Arrows[FindArrow()].GetComponent<EnemeyProjectile>().ActiveProjectiles();

    }

    private int FindArrow()
    {
        for(int i = 0; i < Arrows.Length; i++)
        {
            if (!Arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer>=attackCooldown)
        {
            Attack();
        }
    }
}
