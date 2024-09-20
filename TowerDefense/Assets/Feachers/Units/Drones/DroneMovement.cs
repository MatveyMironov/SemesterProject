using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class DroneMovement
{
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    [field: Space]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public int AngularSpeed { get; private set; }
}
