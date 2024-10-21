using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private PlayerHealth PlayerHealth;
    private UIManager UIManager;
    void Start()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }
    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
    }


    void Update()
    {
      
    }
    public void CheckRespawn()
    {
        if(currentCheckpoint == null) 
        {
            UIManager.GameOver();
            return;
        }
        transform.position = currentCheckpoint.position;
        PlayerHealth.Respawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
