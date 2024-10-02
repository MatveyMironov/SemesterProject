using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [ContextMenuItem("Reset resources", "ResetResources")]
    [SerializeField] private int defaultResourceAmount;
    [SerializeField] private int maxResourceAmount;

    public int ResourceAmount { get; private set; }

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
    }

    private void ResetResource()
    {
        ResourceAmount = defaultResourceAmount;
    }
}
