using TMPro;
using UnityEngine;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private TextMeshProUGUI resourceText;

    private void Start()
    {
        CloseMenu();
    }

    private void OpenMenu()
    {
        tooltip.SetActive(false);
        menuWindow.SetActive(true);
    }

    private void CloseMenu()
    {
        menuWindow.SetActive(false);
        tooltip.SetActive(true);
    }

    private void OnEnable()
    {
        Construction.OnConstructionModeEntered += OpenMenu;
        Construction.OnConstructionModeExited += CloseMenu;
        Resource.OnResourceAmountChanged += DisplayResourceAmount;
    }

    private void OnDisable()
    {
        Construction.OnConstructionModeEntered -= OpenMenu;
        Construction.OnConstructionModeExited -= CloseMenu;
        Resource.OnResourceAmountChanged -= DisplayResourceAmount;
    }

    private void DisplayResourceAmount(int amount)
    {
        resourceText.text = $"Resource: {amount}";
    }
}
