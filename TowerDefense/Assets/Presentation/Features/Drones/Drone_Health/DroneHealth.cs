using System;
using UnityEngine;

[Serializable]
public class DroneHealth : MonoBehaviour
{
    [SerializeField] private Drone _drone;

    [field: SerializeField] public int DefaultHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }

    public event Action OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = DefaultHealth;
        CheckForDeath();
    }

    public void SubtractHealth(int amount)
    {
        if (CurrentHealth < amount)
        {
            CurrentHealth = 0;
        }
        else
        {
            CurrentHealth -= amount;
        }
        
        OnHealthChanged?.Invoke();
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (CurrentHealth <= 0)
        {
            _drone.Die();
        }
    }
}
