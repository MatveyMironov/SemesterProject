using System;
using UnityEngine;

[Serializable]
public class TargetTracking
{
    [field: Tooltip("The part that aims at the target")]
    [field: SerializeField] public Transform Weapon { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }

    public void TrackTarget(Transform target)
    {
        if (target == null)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - Weapon.position);
        Quaternion targetRotation = new Quaternion(0f, lookRotation.y, 0f, lookRotation.w);

        if (Weapon.rotation != lookRotation)
        {
            Weapon.rotation = Quaternion.RotateTowards(Weapon.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }
}
