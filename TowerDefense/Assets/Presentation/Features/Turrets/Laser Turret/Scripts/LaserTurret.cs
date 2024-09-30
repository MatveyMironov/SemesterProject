using UnityEngine;

public class LaserTurret : Turret
{
    [field: SerializeField] public LaserGun LaserGun { get; private set; } = new();

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        Deactivated_State deactivated = new();
        LT_Activated_State activated = new(this);

        _stateMachine.AddTransition(deactivated, activated, () => IsActivated);
        _stateMachine.AddTransition(activated, deactivated, () => !IsActivated);

        _stateMachine.SetState(deactivated);
        _stateMachine.EnterCurrentState();
    }

    private void Update()
    {
        LaserGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
