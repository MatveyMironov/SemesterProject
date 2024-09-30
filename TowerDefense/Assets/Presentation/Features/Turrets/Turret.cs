using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected TurretParametersSO parameters;
    [Space]
    [SerializeField] private Transform detectionSensor;
    [Space]
    [SerializeField] private TargetTracking.TrackingTransforms trackingTransforms = new();
    [Space]
    [SerializeField] public bool IsActivated;

    public TargetTracking TargetTracking { get; private set; }
    public TargetDetection TargetDetection { get; private set; }

    protected virtual void Awake()
    {
        TargetTracking = new(trackingTransforms, parameters.TargetTrackingParameters);
        TargetDetection = new(detectionSensor, parameters.TargetDetectionParameters);
    }

#if UNITY_EDITOR
    [Space]
    [SerializeField] private bool showGizmos;
    [SerializeField] private Color inactiveColor = new Color(1.0f, 1.0f, 1.0f, 0.1f);
    [SerializeField] private Color activeColor = new Color(1.0f, 0.8f, 0f, 0.1f);
    [SerializeField] private Color combatColor = new Color(1.0f, 0f, 0f, 0.1f);

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        if (IsActivated)
        {
            if (TargetDetection != null && TargetDetection.AvailableTargets.Count > 0)
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
        Gizmos.DrawSphere(detectionSensor.position, parameters.TargetDetectionParameters.DetectionRadius);
    }
#endif
}
