public class LaserTurretActivatedState : IState
{
    private LaserTurret _turret;

    private StateMachine _stateMachine;

    public LaserTurretActivatedState(LaserTurret turret)
    {
        _turret = turret;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_turret.DetectionSystem);
        LaserTurretFighting_State fighting = new LaserTurretFighting_State(_turret.DetectionSystem, _turret.TargetTracking, _turret.LaserGun);

        _stateMachine.AddTransition(awaitingOrders, selectingTarget, () => _turret.DetectionSystem.AvailableTargets.Count > 0);
        _stateMachine.AddTransition(selectingTarget, fighting, () => _turret.DetectionSystem.SelectedTarget != null);
        _stateMachine.AddTransition(fighting, awaitingOrders, () => _turret.DetectionSystem.SelectedTarget == null);

        _stateMachine.SetState(awaitingOrders);
    }

    public void OnEnter()
    {
        _stateMachine.EnterCurrentState();
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        _turret.DetectionSystem.UseSensor(_turret.transform, Unit.factions.Rival);

        _stateMachine.Tick();
    }
}
