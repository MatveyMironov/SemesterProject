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
        _droneMovement.OnFinalWaypointReached += AttackPlayerBase;
        _isMoving = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void AttackPlayerBase()
    {
        _program.PlayerBase.DealDamage(droneData.DamageToPlayer);
        Die();
    }

    private void OnDisable()
    {
        _droneMovement.OnFinalWaypointReached -= AttackPlayerBase;
    }

    [Serializable]
    public class DroneProgram
    {
        [field: SerializeField] public Transform[] Waypoints { get; private set; } = new Transform[0];
        [field: SerializeField] public PlayerBase PlayerBase { get; private set; }
    }
}
