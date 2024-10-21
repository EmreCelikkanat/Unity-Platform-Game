using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private UIManager UIManager;


    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.GameOver();
            Time.timeScale = 0;
        }
    }
}
