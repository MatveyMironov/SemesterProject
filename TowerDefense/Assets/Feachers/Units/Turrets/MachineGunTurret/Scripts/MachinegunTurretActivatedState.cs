public class MachinegunTurretActivatedState : IState
{
    private MachinegunTurret _turret;

    private StateMachine _stateMachine;

    public MachinegunTurretActivatedState(MachinegunTurret turret)
    {
        _turret = turret;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_turret.DetectionSystem);
        MachinegunTurretFighting_State fighting = new MachinegunTurretFighting_State(_turret.DetectionSystem, _turret.TargetTracking, _turret.MachineGun);

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
        _turret.MachineGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
