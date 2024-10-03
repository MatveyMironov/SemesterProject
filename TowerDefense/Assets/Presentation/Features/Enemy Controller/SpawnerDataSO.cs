using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnerData", menuName = "Drones/Drone Spawner Data")]
public class SpawnerDataSO : ScriptableObject
{
    [field: SerializeField] public List<DroneDataSO> Drones { get; private set; } = new List<DroneDataSO>();
    [field: SerializeField] public float Distance { get; private set; }
}
