using System;
using UnityEngine;

public class TargetTracking
{
    private TrackingTransforms _transforms;
    private TrackingParameters _parameters;

    public TargetTracking(TrackingTransforms transforms, TrackingParameters parameters)
    {
        _transforms = transforms;
        _parameters = parameters;
    }

    public void TrackTarget(Transform target)
    {
        if (target == null)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - _transforms.Muzzle.position);
        Vector3 rotationEuler = lookRotation.eulerAngles;
        float rotationYClamped = Mathf.Clamp(rotationEuler.y, _parameters.MinHorizontalAngle, _parameters.MaxHorizontalAngle);
        float rotationXClamped = Mathf.Clamp(rotationEuler.x, _parameters.MinVerticalAngle, _parameters.MaxVerticalAngle);
        Quaternion horizontalRotation = Quaternion.Euler(0f, rotationYClamped, 0f);
        Quaternion verticalRotation = Quaternion.Euler(rotationXClamped, 0f, 0f);

        if (_transforms.Muzzle.rotation != lookRotation)
        {
            if (_transforms.HorizontalHinge != null)
            {
                _transforms.HorizontalHinge.localRotation = Quaternion.RotateTowards(_transforms.HorizontalHinge.localRotation, horizontalRotation, _parameters.HorizontalTurningSpeed * Time.deltaTime);
            }
            if (_transforms.VerticalHinge != null)
            {
                _transforms.VerticalHinge.localRotation = Quaternion.RotateTowards(_transforms.VerticalHinge.localRotation, verticalRotation, _parameters.VerticalTurningSpeed * Time.deltaTime);
            }
        }
    }

    [Serializable]
    public class TrackingTransforms
    {
        [field: SerializeField] public Transform Muzzle { get; set; }
        [field: SerializeField] public Transform HorizontalHinge { get; set; }
        [field: SerializeField] public Transform VerticalHinge { get; set; }
    }

    [Serializable]
    public class TrackingParameters
    {
        [field: SerializeField] public float MinHorizontalAngle { get; set; }
        [field: SerializeField] public float MaxHorizontalAngle { get; set; }
        [field: SerializeField] public float HorizontalTurningSpeed { get; set; }
        [field: SerializeField] public float MinVerticalAngle { get; set; }
        [field: SerializeField] public float MaxVerticalAngle { get; set; }
        [field: SerializeField] public float VerticalTurningSpeed { get; set; }
    }
}
