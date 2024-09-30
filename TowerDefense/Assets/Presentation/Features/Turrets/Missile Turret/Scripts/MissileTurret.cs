using UnityEngine;

public class MissileTurret : Turret
{
    [Space]
    [SerializeField] private MT_ParametersSO MTparameters;
    [Space]
    [SerializeField] private MissileLauncher.MissileLauncherComponents missileLauncherComponents;

    public MissileLauncher _missileLauncher { get; private set; }

    private StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();
        _missileLauncher = new(missileLauncherComponents, MTparameters.MissileLauncherParameters);

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
