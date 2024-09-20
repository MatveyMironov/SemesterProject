using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [field: SerializeField] public ConstructionBlueprintSO Blueprint;

    public static event Action<int> OnBuildingDemolished;

    public void DemolishBuilding()
    {
        OnBuildingDemolished?.Invoke(Blueprint.BuildingCost);
    }
}
