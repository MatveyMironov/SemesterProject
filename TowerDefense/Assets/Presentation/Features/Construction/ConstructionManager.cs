using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private List<TurretDataSO> turrets = new();

    [SerializeField] private Construction construction;
    [SerializeField] private ConstructionMenu constructionMenu;

    private void SelectTurret()
    {

    }

    
}

public class ConstructionBlueprint
{
    TurretDataSO turretData;

    public void SelectBlueprint()
    {

    }
}
