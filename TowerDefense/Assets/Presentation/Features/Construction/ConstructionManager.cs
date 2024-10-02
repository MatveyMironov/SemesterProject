using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private List<TurretDataSO> defaultTurrets = new();

    [Space]
    [SerializeField] private Construction construction;
    [SerializeField] private ConstructionMenu constructionMenu;

    private List<TurretDataSO> _availableTurrets = new();

    public bool IsConstructionModeEntered { get; private set; }

    private void Start()
    {
        ExitConstructionMode();

        foreach (var turret in defaultTurrets)
        {
            AddTurret(turret);
        }
    }

    public void EnterConstructionMode()
    {
        construction.ShowConstructionSites();
        constructionMenu.OpenMenu();
        IsConstructionModeEntered = true;
    }

    public void ExitConstructionMode()
    {
        construction.HideConstructionSites();
        construction.AbortBuilding();
        constructionMenu.CloseMenu();
        IsConstructionModeEntered = false;
    }

    public void SelectConstructionSite()
    {
        construction.SelectConstructionSite();
    }

    public void SelectTurret(TurretDataSO turretData)
    {
        construction.SelectTurret(turretData);
    }

    public void AddTurret(TurretDataSO turretData)
    {
        if (CheckIfAlreadyContains(turretData))
        {
            throw new System.Exception("Already Contains that turret");
        }

        ConstructionBlueprint blueprint = new(turretData, this);
        constructionMenu.CreateConstructionButton(blueprint);

        _availableTurrets.Add(turretData);
    }

    private bool CheckIfAlreadyContains(TurretDataSO turretData)
    {
        foreach(TurretDataSO availableTurret in _availableTurrets)
        {
            if (turretData == availableTurret)
            {
                return true;
            }
        }

        return false;
    }
}

public class ConstructionBlueprint
{
    public TurretDataSO TurretData { get; private set; }
    private ConstructionManager _manager;

    public ConstructionBlueprint(TurretDataSO turretData, ConstructionManager manager)
    {
        TurretData = turretData;
        _manager = manager;
    }

    public void SelectBlueprint()
    {
        _manager.SelectTurret(TurretData);
    }
}
