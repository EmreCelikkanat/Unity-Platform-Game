using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;

    [SerializeField] private float speed;
    private Vector3 initScale;
    public  bool movingLeft;

    [SerializeField] private float idleTime;
    private float idleTimer=0f;

    [SerializeField] private Animator anim;

    private void MoveInDirections(int _direction)
    {
        anim.SetBool("moving",true); 
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction*speed, enemy.position.y, enemy.position.z);
    }
    void Start()
    {
        initScale=enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }


    void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x>=leftEdge.position.x)
                MoveInDirections(-1);
            else
            {
                ChangeDirections();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirections(1);
            }
            else
            {
                ChangeDirections();
            }
        }
    }
    private void ChangeDirections()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleTime)
        {
            idleTimer = 0;
            movingLeft = !movingLeft;
        }
            
        
    }
}
