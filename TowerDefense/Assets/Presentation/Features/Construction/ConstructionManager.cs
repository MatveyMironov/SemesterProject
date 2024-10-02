using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private List<TurretDataSO> turrets = new();

    [SerializeField] private Construction construction;
    [SerializeField] private ConstructionMenu constructionMenu;

    public bool IsConstructionModeEntered { get; private set; }

    private void Start()
    {
        CreateBlueprints();
        ExitConstructionMode();
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

    private void CreateBlueprints()
    {
        foreach (var turret in turrets)
        {
            ConstructionBlueprint blueprint = new(turret, this);
            constructionMenu.CreateConstructionButton(blueprint);
        }
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
