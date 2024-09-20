using UnityEngine;

public class BuildingRemovement
{
    public void RemoveBuilding(Building building)
    {
        Object.Destroy(building.gameObject);
    }
}
