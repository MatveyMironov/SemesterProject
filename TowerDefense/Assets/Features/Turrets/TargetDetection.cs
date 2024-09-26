using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetDetection
{
    [field: Tooltip("If you want the sensor to be not in the center of the object")]
    [field: SerializeField] public Vector3 SensorOffset { get; private set; }
    [field: SerializeField] public float DetectionRadius { get; private set; }

    public List<Drone> AvailableTargets { get; private set; } = new List<Drone>();
    public Drone SelectedTarget { get; set; }

    public void UseSensor(Transform sensor)
    {
        DetectTargets(sensor);
        CheckSelectedTarget();
    }

    private void DetectTargets(Transform sensor)
    {
        List<Drone> detectedTargets = new List<Drone>();

        Collider[] possibleTargets = Physics.OverlapSphere(sensor.position + SensorOffset, DetectionRadius);
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
}
