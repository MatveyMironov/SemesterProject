using UnityEngine;

[CreateAssetMenu(fileName = "NewDroneData", menuName = "Drones/Drone Data")]
public class DroneDataSO : ScriptableObject
{
    [field: SerializeField] public Drone Prefab { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
}
