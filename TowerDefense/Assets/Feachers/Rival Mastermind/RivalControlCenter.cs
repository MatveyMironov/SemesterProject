using System.Collections.Generic;
using UnityEngine;

public class RivalControlCenter : MonoBehaviour
{
    [SerializeField] private Transform mainTarget;
    [SerializeField] private List<GunshipDrone> drones;

    private void Start()
    {
        AssignTargets();
    }

    private void AssignTargets()
    {
        foreach (var drone in drones)
        {
            drone.DroneOrders.RequiredDestination = mainTarget.position;
            drone.IsActivated = true;
        }
    }
}
