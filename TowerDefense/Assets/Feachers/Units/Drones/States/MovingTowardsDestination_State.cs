using UnityEngine;

public class MovingTowardsDestination_State : IState
{
    private DroneMovement _droneMovement;
    private DroneOrders _droneOrders;

    public MovingTowardsDestination_State(DroneMovement droneMovement, DroneOrders droneOrders)
    {
        _droneMovement = droneMovement;
        _droneOrders = droneOrders;
    }

    public void OnEnter()
    {
        Debug.Log("Moving to coordinates");
        _droneMovement.Agent.speed = _droneMovement.MovementSpeed;
        _droneMovement.Agent.angularSpeed = _droneMovement.AngularSpeed;
        _droneMovement.Agent.destination = _droneOrders.RequiredDestination;
        _droneMovement.Agent.isStopped = false;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }

    public bool HasReachedDestination()
    {
        return _droneMovement.Agent.remainingDistance <= 1;
    }
}
