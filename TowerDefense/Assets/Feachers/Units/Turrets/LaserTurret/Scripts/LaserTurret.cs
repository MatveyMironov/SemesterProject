using UnityEngine;

public class LaserTurret : Turret
{
    [field: SerializeField] public BeamGun LaserGun { get; private set; } = new BeamGun();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        DeactivatedState deactivatedState = new DeactivatedState();
        LaserTurretActivatedState activatedState = new LaserTurretActivatedState(this);

        _stateMachine.AddTransition(deactivatedState, activatedState, () => IsActivated);
        _stateMachine.AddTransition(activatedState, deactivatedState, () => !IsActivated);

        _stateMachine.SetState(deactivatedState);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        LaserGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
