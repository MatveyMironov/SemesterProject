using UnityEngine;

public class PlasmaTurret : Turret
{
    [field: Space]
    [field: SerializeField] public PlasmaGun PlasmaGun { get; private set; } = new();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new();

        Deactivated_State deactivated = new();
        PT_Activated_State activated = new(this);

        _stateMachine.AddTransition(deactivated, activated, () => IsActivated);
        _stateMachine.AddTransition(activated, deactivated, () => !IsActivated);

        _stateMachine.SetState(deactivated);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        PlasmaGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
