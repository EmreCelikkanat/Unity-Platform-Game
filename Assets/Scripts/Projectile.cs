using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : DealDamage
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private BoxCollider2D BoxCollider2D;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if(hit) return; 
        float movementSpeed = speed * Time.deltaTime*direction;
        transform.Translate(movementSpeed,0,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        base.OnTriggerEnter2D(collision);
        hit = true;
        BoxCollider2D.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        float localScaleX=transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
     public  void Deactive()
    {
        gameObject.SetActive(false);
    }
    public void ActivateProjectiles()
    {
        hit = false;
        gameObject.SetActive(true);
        BoxCollider2D.enabled = true;
    }
}
