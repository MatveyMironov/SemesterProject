using UnityEngine;

public abstract class TurretDataSO : ScriptableObject
{
    [field: SerializeField] public string TurretName { get; private set; }
    [field: SerializeField] public int TurretCost { get; private set; }
    [field: SerializeField] public Turret TurretPrefab { get; private set; }
    [field: SerializeField] public GameObject TurretPreview { get; private set; }
    [field: SerializeField] public TargetDetection.DetectionParameters TargetDetectionParameters { get; private set; }
    [field: SerializeField] public TargetTracking.TrackingParameters TargetTrackingParameters { get; private set; }
}
