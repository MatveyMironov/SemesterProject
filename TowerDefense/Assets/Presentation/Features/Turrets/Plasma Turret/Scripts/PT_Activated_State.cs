public class PT_Activated_State : IState
{
    private PlasmaTurret _turret;

    private StateMachine _stateMachine;

    public PT_Activated_State(PlasmaTurret turret)
    {
        _turret = turret;

        _stateMachine = new StateMachine();

        AwaitingOrders_State awaitingOrders = new AwaitingOrders_State();
        SelectingTarget_State selectingTarget = new SelectingTarget_State(_turret.TargetDetection);
        PT_Fighting_State fighting = new PT_Fighting_State(_turret.TargetDetection, _turret.TargetTracking, _turret._plasmaGun);

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
        _turret.TargetDetection.UseSensor();

        _stateMachine.Tick();
    }
}
