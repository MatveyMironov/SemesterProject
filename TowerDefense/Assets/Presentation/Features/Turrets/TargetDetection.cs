using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection
{
    private Transform _sensor;
    private DetectionParameters _parameters;

    public TargetDetection(Transform sensor, DetectionParameters parameters)
    {
        _sensor = sensor;
        _parameters = parameters;
    }

    public List<Drone> AvailableTargets { get; private set; } = new();
    public Drone SelectedTarget { get; set; }

    public void UseSensor()
    {
        DetectTargets();
        CheckSelectedTarget();
    }

    private void DetectTargets()
    {
        List<Drone> detectedTargets = new List<Drone>();

        Collider[] possibleTargets = Physics.OverlapSphere(_sensor.position, _parameters.DetectionRadius);
        foreach (Collider possibleTarget in possibleTargets)
        {
            if (possibleTarget.TryGetComponent(out Drone potentialTarget))
            {
                detectedTargets.Add(potentialTarget);
            }
        }

        AvailableTargets = detectedTargets;
    }

    private void CheckSelectedTarget()
    {
        if (!AvailableTargets.Contains(SelectedTarget))
        {
            SelectedTarget = null;
        }
    }

    [Serializable]
    public class DetectionParameters
    {
        [field: SerializeField] public float DetectionRadius { get; private set; }
    }
}
