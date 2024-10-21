
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image totalHealthBar;


    private void Start()
    {
        totalHealthBar.fillAmount = health.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = health.currentHealth / 10;
    }
}
