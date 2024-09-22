using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private DroneMovement droneMovement;

    private void Update()
    {
        droneMovement.Tick();
    }
}
