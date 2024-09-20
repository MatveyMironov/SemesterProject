using UnityEngine;


public class GunshipDroneActivatedState : IState
{
    private GunshipDrone _drone;

    private StateMachine _stateMachine;

    public GunshipDroneActivatedState(GunshipDrone drone)
    {
        _drone = drone;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        MovingTowardsDestination_State movingTowardsDestination = new MovingTowardsDestination_State(_drone.DroneMovement, _drone.DroneOrders);
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_drone.DetectionSystem);
        GunshipDroneFighting_State fighting = new GunshipDroneFighting_State(_drone.DetectionSystem, _drone.DroneMovement, _drone.TargetTracking, _drone.MachineGun);

        _stateMachine.AddTransition(awaitingOrders, movingTowardsDestination, () => _drone.DroneOrders.RequiredDestination != null);
        _stateMachine.AddTransition(movingTowardsDestination, selectingTarget, () => _drone.DetectionSystem.AvailableTargets.Count > 0);
        _stateMachine.AddTransition(selectingTarget, fighting, () => _drone.DetectionSystem.SelectedTarget != null);
        _stateMachine.AddTransition(fighting, awaitingOrders, () => _drone.DetectionSystem.SelectedTarget == null);

        _stateMachine.SetState(awaitingOrders);
    }

    public void OnEnter()
    {
        Debug.Log("Executing program");
        _stateMachine.EnterCurrentState();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        _drone.DetectionSystem.UseSensor(_drone.transform, Unit.factions.Player);
        _drone.MachineGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
