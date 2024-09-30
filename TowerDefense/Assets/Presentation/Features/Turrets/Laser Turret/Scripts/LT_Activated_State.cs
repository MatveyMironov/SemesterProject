public class LT_Activated_State : IState
{
    private LaserTurret _turret;

    private StateMachine _stateMachine;

    public LT_Activated_State(LaserTurret turret)
    {
        _turret = turret;

        _stateMachine = new();

        AwaitingOrders_State awaitingOrders = new();
        SelectingTarget_State selectingTarget = new(_turret.TargetDetection);
        LT_Fighting_State fighting = new(_turret.TargetDetection, _turret.TargetTracking, _turret.LaserGun);

        _stateMachine.AddTransition(awaitingOrders, selectingTarget, () => _turret.TargetDetection.AvailableTargets.Count > 0);
        _stateMachine.AddTransition(selectingTarget, fighting, () => _turret.TargetDetection.SelectedTarget != null);
        _stateMachine.AddTransition(fighting, awaitingOrders, () => _turret.TargetDetection.SelectedTarget == null);

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
