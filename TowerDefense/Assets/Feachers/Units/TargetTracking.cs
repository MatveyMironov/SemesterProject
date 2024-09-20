using System;
using UnityEngine;

[Serializable]
public class TargetTracking
{
    [field: Tooltip("The part that aims at the target")]
    [field: SerializeField] public Transform Weapon { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }

    private Quaternion _lookAtRotation;

    public void TrackTarget(Transform target)
    {
        if (target != null)
        {
            _lookAtRotation = Quaternion.LookRotation(target.transform.position - Weapon.position);
            if (Weapon.rotation != _lookAtRotation)
            {
                Weapon.rotation = Quaternion.RotateTowards(Weapon.rotation, _lookAtRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }
}
