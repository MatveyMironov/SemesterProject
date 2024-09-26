using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    [SerializeField] private Button laserTurretButton;
    [SerializeField] private Button missileTurretButton;
    [SerializeField] private Button plasmaTurretButton;

    public event Action OnLaserTurretClicked;
    public event Action OnMissileTurretClicked;
    public event Action OnPlasmaTurretClicked;

    private void Awake()
    {
        CloseMenu();

        laserTurretButton.onClick.AddListener(() => OnLaserTurretClicked?.Invoke());
        missileTurretButton.onClick.AddListener(() => OnMissileTurretClicked?.Invoke());
        plasmaTurretButton.onClick.AddListener(() => OnPlasmaTurretClicked?.Invoke());
    }

    public void OpenMenu()
    {
        menuWindow.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindow.SetActive(false);
    }
}
