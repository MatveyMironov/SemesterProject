using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [ContextMenuItem("Reset Available Turrets", "ResetAvailableTurrets")]
    [SerializeField] private List<TurretDataSO> defaultTurrets = new();

    [Space]
    [SerializeField] private Construction construction;
    [SerializeField] private ConstructionMenu constructionMenu;

    private List<ConstructionBlueprint> _availableBlueprints = new();

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

    public void SelectBlueprint(ConstructionBlueprint blueprint)
    {
        construction.SelectTurret(blueprint.TurretData);
    }

    public void AddTurret(TurretDataSO turretData)
    {
        ConstructionBlueprint blueprint = _availableBlueprints.Find(turret => turret.TurretData == turretData);

        if (blueprint != null)
        {
            throw new System.Exception("Available turrets already contain such turret");
        }

        blueprint = new(turretData);
        blueprint.OnBlueprintSelected += SelectBlueprint;
        _availableBlueprints.Add(blueprint);
        constructionMenu.AddConstructionButton(blueprint);
    }

    public void RemoveTurret(TurretDataSO turretData)
    {
        ConstructionBlueprint blueprint = _availableBlueprints.Find(blueprint => blueprint.TurretData == turretData);

        if (blueprint == null)
        {
            throw new System.Exception("No such turret in available turrets");
        }

        constructionMenu.RemoveConstructionButton(blueprint);
        _availableBlueprints.Remove(blueprint);

        Debug.Log("removed");
    }

    public void ResetAvailableTurrets()
    {
        for (int i = _availableBlueprints.Count - 1; i >= 0; i--)
        {
            RemoveTurret(_availableBlueprints[i].TurretData);
        }

        foreach (var turret in defaultTurrets)
        {
            AddTurret(turret);
        }
    }
}

public class ConstructionBlueprint
{
    public TurretDataSO TurretData { get; private set; }

    public Action<ConstructionBlueprint> OnBlueprintSelected;

    public ConstructionBlueprint(TurretDataSO turretData)
    {
        TurretData = turretData;
    }
}
