public class MT_Activated_State : IState
{
    private MissileTurret _turret;

    private StateMachine _stateMachine;

    public MT_Activated_State(MissileTurret turret)
    {
        _turret = turret;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_turret.TargetDetection);
        MT_Fighting_State fihting = new MT_Fighting_State(_turret.TargetDetection, _turret.TargetTracking, _turret.MissileLauncher);

        _stateMachine.AddTransition(awaitingOrders, selectingTarget, () => _turret.TargetDetection.AvailableTargets.Count > 0);
        _stateMachine.AddTransition(selectingTarget, fihting, () => _turret.TargetDetection.SelectedTarget != null);
        _stateMachine.AddTransition(fihting, awaitingOrders, () => _turret.TargetDetection.SelectedTarget == null);

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
        _turret.TargetDetection.UseSensor(_turret.transform);

        _stateMachine.Tick();
    }
}
