using System;
using UnityEngine;

[Serializable]
public class PlayerHealth
{
    [field: SerializeField] public int DefaultHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public event Action OnHealthAmountChanged;
    public event Action OnHealthExpired;

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

        OnHealthAmountChanged?.Invoke();

        if (CurrentHealth <= 0)
        {
            OnHealthExpired?.Invoke();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = DefaultHealth;
    }
}
