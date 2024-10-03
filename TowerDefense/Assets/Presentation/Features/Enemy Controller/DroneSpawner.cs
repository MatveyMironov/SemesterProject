using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerDataSO spawnerData;
    [Space]
    [SerializeField] private Transform spawnPoint;
    [Space]
    [SerializeField] private Drone.DroneProgram program;

    // So that drones always spawn in correct distance from each other
    private Vector3 _adjustedSpawnPosition;
    private Quaternion _adjustedSpawnRotation;
    private Transform _lastSpawnedTransform = null;

    private int _nextDroneIndex = 0;

    private void Awake()
    {
        _adjustedSpawnPosition = spawnPoint.position;
        _adjustedSpawnRotation = spawnPoint.rotation;
    }

    private void FixedUpdate()
    {
        if (_nextDroneIndex >= spawnerData.Drones.Count)
            return;

        if (_lastSpawnedTransform != null)
        {
            Vector3 DroneToSpawn = spawnPoint.position - _lastSpawnedTransform.position;
            if (DroneToSpawn.magnitude < spawnerData.Distance)
            {
                return;
            }

            _adjustedSpawnPosition = _lastSpawnedTransform.position + DroneToSpawn.normalized * spawnerData.Distance;

            // TODO:
            // Somehow calculate _adjustedSpawnRotation. Not critical because it probably would always be the same.
        }

        SpawnDrone();
    }

    private void SpawnDrone()
    {
        Drone newDrone = Instantiate(spawnerData.Drones[_nextDroneIndex].Prefab, _adjustedSpawnPosition, _adjustedSpawnRotation);
        newDrone.SetProgram(program);
        _lastSpawnedTransform = newDrone.transform;
        _nextDroneIndex++;
    }

    [ContextMenu("Reset Spawner")]
    private void ResetSpawner()
    {
        _nextDroneIndex = 0;
        _lastSpawnedTransform = null;
    }
}
