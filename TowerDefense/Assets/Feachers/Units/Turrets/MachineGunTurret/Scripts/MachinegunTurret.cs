using UnityEngine;

public class MachinegunTurret : Turret
{
    [field: SerializeField] public MachineGun MachineGun { get; private set; } = new MachineGun();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        DeactivatedState deactivatedState = new DeactivatedState();
        MachinegunTurretActivatedState activatedState = new MachinegunTurretActivatedState(this);

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
