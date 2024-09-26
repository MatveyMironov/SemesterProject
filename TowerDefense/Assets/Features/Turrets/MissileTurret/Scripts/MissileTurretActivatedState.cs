public class MissileTurretActivatedState : IState
{
    private MissileTurret _turret;

    private StateMachine _stateMachine;

    public MissileTurretActivatedState(MissileTurret turret)
    {
        _turret = turret;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_turret.DetectionSystem);
        FiringMissileLauncher_State firingBeamGun = new FiringMissileLauncher_State(_turret.DetectionSystem, _turret.TargetTracking, _turret.MissileLauncher);

        _stateMachine.AddTransition(awaitingOrders, selectingTarget, () => _turret.DetectionSystem.AvailableTargets.Count > 0);
        _stateMachine.AddTransition(selectingTarget, firingBeamGun, () => _turret.DetectionSystem.SelectedTarget != null);
        _stateMachine.AddTransition(firingBeamGun, awaitingOrders, () => _turret.DetectionSystem.SelectedTarget == null);

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
        _turret.DetectionSystem.UseSensor(_turret.transform);

        _stateMachine.Tick();
    }
}
