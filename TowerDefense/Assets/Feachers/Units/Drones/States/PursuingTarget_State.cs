using UnityEngine;

public class PursuingTarget_State : IState
{
    private DroneMovement _droneMovement;
    private Transform _target;
    private Vector3 _desiredDistance;

    public PursuingTarget_State(DroneMovement droneMovement, Transform target, Vector3 desiredDistance)
    {
        _droneMovement = droneMovement;
        _target = target;
        _desiredDistance = desiredDistance;
    }

    public void OnEnter()
    {
        Debug.Log("Persuing target");
        _droneMovement.Agent.speed = _droneMovement.MovementSpeed;
        _droneMovement.Agent.angularSpeed = _droneMovement.AngularSpeed;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        _droneMovement.Agent.SetDestination(_target.position);
    }
}
