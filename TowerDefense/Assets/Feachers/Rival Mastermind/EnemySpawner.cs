using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GunshipDrone machineGunDronePrefab;
    [SerializeField] private Transform[] positionsToSpawn;
    [SerializeField] private Transform playerBase;

    public void SpawnWave()
    {
        foreach (Transform position in positionsToSpawn)
        {
            GunshipDrone drone = Instantiate(machineGunDronePrefab, position.position, Quaternion.identity);
            drone.IsActivated = true;
            drone.DroneOrders.RequiredDestination = playerBase.position;
        }
    }
}
