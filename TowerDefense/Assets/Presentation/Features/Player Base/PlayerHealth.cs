using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int defaultHealth;

    public int CurrentHealth { get; private set; }

    public void RemoveHealth(int amount)
    {
        if (CurrentHealth < amount)
        {
            CurrentHealth = 0;
        }
        else
        {
            CurrentHealth -= amount;
        }

        if (CurrentHealth <= 0)
        {

        }
    }

    private void DestroyBase()
    {

    }
}
