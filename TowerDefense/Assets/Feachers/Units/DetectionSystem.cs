using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DetectionSystem
{
    [field: Tooltip("If you want the sensor to be not in the center of the object")]
    [field: SerializeField] public Vector3 SensorOffset { get; private set; }
    [field: SerializeField] public float DetectionRadius { get; private set; }

    public List<Unit> AvailableTargets { get; private set; } = new List<Unit>();
    public Unit SelectedTarget { get; set; }

    public void UseSensor(Transform sensor, Unit.factions faction)
    {
        DetectTargets(sensor, faction);
        CheckSelectedTarget();
    }

    private void DetectTargets(Transform sensor, Unit.factions faction)
    {
        List<Unit> detectedTargets = new List<Unit>();

        Collider[] possibleTargets = Physics.OverlapSphere(sensor.position + SensorOffset, DetectionRadius);
        foreach (Collider possibleTarget in possibleTargets)
        {
            if (possibleTarget.TryGetComponent(out Unit potentialTarget))
            {
                if (potentialTarget.Faction == faction)
                {
                    detectedTargets.Add(potentialTarget);
                }
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
