using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private List<TurretDataSO> turrets = new();

    [SerializeField] private Construction construction;
    [SerializeField] private ConstructionMenu constructionMenu;

    private void Start()
    {
        CreateBlueprints();
    }

    public void SelectTurret(TurretDataSO turretData)
    {

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
