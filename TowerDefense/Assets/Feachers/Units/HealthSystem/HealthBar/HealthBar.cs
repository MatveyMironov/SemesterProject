using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBarFill;

    private void ShowHealth()
    {
        healthBarFill.fillAmount = (float) health.CurrentHealth / health.DefaultHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChanged += ShowHealth;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= ShowHealth;
    }
}
