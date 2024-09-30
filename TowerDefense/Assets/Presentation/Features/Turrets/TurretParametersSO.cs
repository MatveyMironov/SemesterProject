using UnityEngine;

public abstract class TurretParametersSO : ScriptableObject
{
    [field: SerializeField] public TargetDetection.DetectionParameters TargetDetectionParameters { get; private set; }
    [field: SerializeField] public TargetTracking.TrackingParameters TargetTrackingParameters { get; private set; }
}
