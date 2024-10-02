using UnityEngine;

public class LaserTurret : Turret
{
    [Space]
    [SerializeField] private LTDataSO LTparameters;
    [Space]
    [SerializeField] private LaserGun.LaserGunComponents laserGunComponents;

    public LaserGun _laserGun { get; private set; }

    private StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();
        _laserGun = new(laserGunComponents, LTparameters.LaserGunParameters);

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
        _laserGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
