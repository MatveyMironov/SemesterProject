using System;
using UnityEngine;

namespace ResourceSystem
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private int defaultResourceAmount;
        [SerializeField] private int maxResourceAmount;

        public int ResourceAmount { get; private set; }

        public event Action OnResourceAmountChanged;

        private void Start()
        {
            ResetResource();
        }

        public void AddResource(int amount)
        {
            if (ResourceAmount + amount > maxResourceAmount)
            {
                ResourceAmount = maxResourceAmount;
            }
            else
            {
                ResourceAmount += amount;
            }

            OnResourceAmountChanged?.Invoke();
        }

        public void SubtractResource(int amount)
        {
            if (amount > ResourceAmount)
            {
                ResourceAmount = 0;
            }
            else
            {
                ResourceAmount -= amount;
            }

            OnResourceAmountChanged?.Invoke();
        }

        [ContextMenu("ResetResources")]
        private void ResetResource()
        {
            ResourceAmount = defaultResourceAmount;

            OnResourceAmountChanged?.Invoke();
        }
    }
}
