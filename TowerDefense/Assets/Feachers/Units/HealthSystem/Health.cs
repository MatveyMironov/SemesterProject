using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int DefaultHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }

    [SerializeField] private Unit unit;

    public event Action OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = DefaultHealth;
        CheckHealth();
    }

    public void DealDamage(int damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke();
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            unit.Die();
        }
    }
}
