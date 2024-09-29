using UnityEngine;

public class PlasmaTurret : Turret
{
    [field: SerializeField] public PlasmaGun PlasmaGun { get; private set; } = new PlasmaGun();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        DeactivatedState deactivatedState = new DeactivatedState();
        PT_Activated_State activatedState = new PT_Activated_State(this);

        _stateMachine.AddTransition(deactivatedState, activatedState, () => IsActivated);
        _stateMachine.AddTransition(activatedState, deactivatedState, () => !IsActivated);

        _stateMachine.SetState(deactivatedState);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        PlasmaGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
