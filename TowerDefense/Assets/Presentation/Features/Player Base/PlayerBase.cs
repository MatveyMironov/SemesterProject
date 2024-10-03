using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private GameObject baseObject;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerHealthBar healthbar;

    private void DestroyBase()
    {
        baseObject.SetActive(false);
    }

    public void DealDamage(int amount)
    {
        health.SubtractHealth(amount);
    }

    private void DisplayHealthEffects()
    {
        healthbar.DisplayHealth(health.CurrentHealth / health.DefaultHealth);
    }

    private void OnEnable()
    {
        health.OnHealthAmountChanged += DisplayHealthEffects;
        health.OnHealthExpired += DestroyBase;
    }

    private void OnDisable()
    {
        health.OnHealthAmountChanged -= DisplayHealthEffects;
        health.OnHealthExpired -= DestroyBase;
    }
}
