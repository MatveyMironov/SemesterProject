using System;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    [SerializeField] private Button laserTurretButton;
    [SerializeField] private Button missileTurretButton;
    [SerializeField] private Button plasmaTurretButton;

    public static event Action OnLaserTurretClicked;
    public static event Action OnMissileTurretClicked;
    public static event Action OnPlasmaTurretClicked;

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

    private void OnEnable()
    {
        Construction.OnConstructionModeEntered += OpenMenu;
        Construction.OnConstructionModeExited += CloseMenu;
    }

    private void OnDisable()
    {
        Construction.OnConstructionModeEntered -= OpenMenu;
        Construction.OnConstructionModeExited -= CloseMenu;
    }
}
