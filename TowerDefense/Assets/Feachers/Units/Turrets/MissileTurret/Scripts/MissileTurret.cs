using UnityEngine;

public class MissileTurret : Turret
{
    [field: SerializeField] public MissileLauncher MissileLauncher { get; private set; } = new MissileLauncher();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        DeactivatedState deactivatedState = new DeactivatedState();
        MissileTurretActivatedState activatedState = new MissileTurretActivatedState(this);

        _stateMachine.AddTransition(deactivatedState, activatedState, () => IsActivated);
        _stateMachine.AddTransition(activatedState, deactivatedState, () => !IsActivated);

        _stateMachine.SetState(deactivatedState);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}
