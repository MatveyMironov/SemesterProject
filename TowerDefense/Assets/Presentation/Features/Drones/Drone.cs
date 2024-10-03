using System;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private DroneDataSO droneData;
    
    private DroneProgram _program;
    private DroneMovement _droneMovement;
    private bool _isMoving;

    private void Update()
    {
        if (_isMoving)
            _droneMovement.MovementTick();
    }

    public void SetProgram(DroneProgram program)
    {
        _program = program;
        _droneMovement = new(transform, program, droneData);
        _isMoving = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

[Serializable]
public class DroneProgram
{
    [SerializeField] public Transform[] Waypoints = new Transform[0];
}
