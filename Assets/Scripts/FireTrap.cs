using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private bool trigerred;
    private bool active;

    private float delay=1f;
    private float activeTime=2f;

    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerHealth health;
    private float damageTimer;
    [SerializeField] private float damageCounter;

    [SerializeField] AudioClip firesound;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        damageTimer += Time.deltaTime;
        if (active && health!=null && damageTimer>damageCounter) 
        {
            health.TakeDamage(1);
            damageTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!trigerred)
            {
                StartCoroutine(Trap());
            } 
            health=collision.GetComponent<PlayerHealth>(); 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        health = null;
    }

    private IEnumerator Trap() 
    {
        trigerred = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(delay);
        SoundManager.instance.PlaySound(firesound);
        sprite.color = Color.white;
        active = true;
        anim.SetBool("activated",true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        trigerred = false;
        anim.SetBool("activated", false);
    }
}
