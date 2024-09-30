using UnityEngine;

public class MissileTurret : Turret
{
    [field: Space]
    [field: SerializeField] public MissileLauncher MissileLauncher { get; private set; } = new();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new();

        Deactivated_State deactivated = new();
        MT_Activated_State activated = new(this);

        _stateMachine.AddTransition(deactivated, activated, () => IsActivated);
        _stateMachine.AddTransition(activated, deactivated, () => !IsActivated);

        _stateMachine.SetState(deactivated);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}
