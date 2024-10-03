using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbarFill;

    public void DisplayHealth(float fraction)
    {
        healthbarFill.fillAmount = fraction;
    }
}
