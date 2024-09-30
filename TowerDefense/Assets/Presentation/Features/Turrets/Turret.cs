using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [field: Space]
    [field: SerializeField] public TargetDetection TargetDetection { get; private set; } = new();
    [field: Space]
    [field: SerializeField] public TargetTracking TargetTracking { get; private set; } = new();

    [field: Space]
    [field: SerializeField] public bool IsActivated;

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
            if (TargetDetection.AvailableTargets.Count > 0)
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
        Gizmos.DrawSphere(transform.position + TargetDetection.SensorOffset, TargetDetection.DetectionRadius);
    }
#endif
}
