using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyProjectile : EnemyDamage
{
    [SerializeField] private float speed=4f;
    [SerializeField] private float resetTime = 2f;
    private float lifeTime;
    private float direction;
    [SerializeField] private BoxCollider2D box;
   
    private Animator anim;

    private bool hit;
    public void ActiveProjectiles()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        box.enabled = true;
    }
   
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        
        lifeTime += Time.deltaTime;
        if(lifeTime>resetTime) 
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit= true;
        base.OnTriggerEnter2D(collision);
        box.enabled= false;
         if(anim!= null)
        {
            anim.SetTrigger("explode");
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
    private void Start()
    {
       box = GetComponent<BoxCollider2D>();
       anim = GetComponent<Animator>();
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
    }
    public void Direction(float _direction)
    {
        direction= _direction;
        
        float localScaleX=transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX *= -1; 
        }
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
}
