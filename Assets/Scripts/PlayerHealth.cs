using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth=3f;
    public float currentHealth { get; private set; }


    [SerializeField] private float IFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private Animator anim;

    [SerializeField] private Behaviour[] components;

    [SerializeField] private AudioClip deathsound;
    [SerializeField] private AudioClip hurtsound;
    void Awake()
    {
        currentHealth = maxHealth;  
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim.SetBool("dead",false);
    }
    public void TakeDamage(float damage)
    {
        currentHealth=Mathf.Clamp(currentHealth-damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtsound);
            
        }
        else
        {
            anim.SetBool("dead", true);
            anim.SetTrigger("die");
           foreach(Behaviour comp in components)
            {
                comp.enabled = false;
            }
           SoundManager.instance.PlaySound(deathsound);
        }
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 8, true); 
        for(int i=0; i<numberOfFlashes; i++)
        {
            spriteRend.color= new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(IFrameDuration/(numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new  WaitForSeconds(IFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 8, false);
    }
    public void AddHealth(float value)
    {
        currentHealth=Mathf.Clamp(currentHealth+value, 0, maxHealth);
    }
    public void Respawn()
    {
        anim.SetBool("dead", false);
        AddHealth(maxHealth);
        anim.ResetTrigger("die");
       anim.Play("idleAnim");
        StartCoroutine(Invunerability());
        foreach (Behaviour comp in components)
        {
            comp.enabled = true;
        }
    }
}
