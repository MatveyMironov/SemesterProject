using System;
using UnityEngine;

[Serializable]
public class TargetTracking
{
    [Tooltip("The part that aims at the target")]
    [SerializeField] private Transform muzzle;
    [Tooltip("The part that rotates in horizontal plane")]
    [SerializeField] private Transform horizontalHinge;
    [SerializeField] private float horizontalRotationSpeed;
    [Tooltip("The part that rotates in vertical plane")]
    [SerializeField] private Transform verticalHinge;
    [SerializeField] private float verticalRotationSpeed;

    public void TrackTarget(Transform target)
    {
        if (target == null)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - muzzle.position);
        Vector3 rotationEuler = lookRotation.eulerAngles;
        Quaternion horizontalRotation = Quaternion.Euler(0f, rotationEuler.y, 0f);
        Quaternion verticalRotation = Quaternion.Euler(rotationEuler.x, 0f, 0f);

        if (muzzle.rotation != lookRotation)
        {
            horizontalHinge.localRotation = Quaternion.RotateTowards(horizontalHinge.localRotation, horizontalRotation, horizontalRotationSpeed * Time.deltaTime);
            verticalHinge.localRotation = Quaternion.RotateTowards(verticalHinge.localRotation, verticalRotation, verticalRotationSpeed * Time.deltaTime);
        }
    }
}
