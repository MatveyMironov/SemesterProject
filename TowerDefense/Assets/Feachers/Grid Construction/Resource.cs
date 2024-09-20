using System;
using UnityEngine;

[Serializable]
public class Resource
{
    [field: SerializeField] public int ResourceAmount { get; private set; }

    public static event Action<int> OnResourceAmountChanged;

    public void AddResource(int amount)
    {
        ResourceAmount += amount;

        OnResourceAmountChanged?.Invoke(ResourceAmount);
    }

    public bool SubtractResource(int amount)
    {
        if (amount > ResourceAmount)
        {
            return false;
        }

        ResourceAmount -= amount;
        OnResourceAmountChanged?.Invoke(ResourceAmount);
        return true;
    }
}
