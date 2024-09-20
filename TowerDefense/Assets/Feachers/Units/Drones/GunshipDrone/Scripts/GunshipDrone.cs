using UnityEngine;

public class GunshipDrone : Unit
{
    [field: SerializeField] public DroneMovement DroneMovement { get; private set; } = new DroneMovement();
    [field: SerializeField] public DetectionSystem DetectionSystem { get; private set; } = new DetectionSystem();
    [field: SerializeField] public TargetTracking TargetTracking { get; private set; } = new TargetTracking();
    [field: SerializeField] public MachineGun MachineGun { get; private set; } = new MachineGun();

    public DroneOrders DroneOrders { get; private set; } = new DroneOrders();

    public bool IsActivated;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        DeactivatedState deactivatedState = new DeactivatedState();
        GunshipDroneActivatedState activatedState = new GunshipDroneActivatedState(this);

        _stateMachine.AddTransition(deactivatedState, activatedState, () => IsActivated);
        _stateMachine.AddTransition(activatedState, deactivatedState, () => !IsActivated);

        _stateMachine.SetState(deactivatedState);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

#if UNITY_EDITOR
    [Space]
    [SerializeField] private bool showGizmos;
    [SerializeField] private Color inactiveColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    [SerializeField] private Color activeColor = new Color(1.0f, 0.8f, 0f, 0.5f);
    [SerializeField] private Color combatColor = new Color(1.0f, 0f, 0f, 0.5f);

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        if (IsActivated)
        {
            if (DetectionSystem.AvailableTargets.Count > 0)
            {
                Gizmos.color = combatColor;
            }
            else
            {
                Gizmos.color = activeColor;
            }
        }
        else
        {
            Gizmos.color = inactiveColor;
        }
        Gizmos.DrawSphere(transform.position + DetectionSystem.SensorOffset, DetectionSystem.DetectionRadius);
    }
#endif
}
