using UnityEngine;

public class PlasmaTurret : Turret
{
    [Space]
    [SerializeField] private PTDataSO PTparameters;
    [Space]
    [SerializeField] private PlasmaGun.PlasmaGunComponents plasmaGunComponents;

    public PlasmaGun _plasmaGun { get; private set; }

    private StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();
        _plasmaGun = new(plasmaGunComponents, PTparameters.PlasmaGunParameters);

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
        _plasmaGun.FunctioningTick();

        _stateMachine.Tick();
    }
}
