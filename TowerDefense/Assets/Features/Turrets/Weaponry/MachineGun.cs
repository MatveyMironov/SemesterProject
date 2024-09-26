using System;
using UnityEngine;

[Serializable]
public class MachineGun
{
    [field: SerializeField] public Transform Muzzle { get; private set; }
    [field: Tooltip("Shots per Second")]
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    [field: Header("Effects")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [field: SerializeField] public AudioClip ShotSound { get; private set; }
    [field: SerializeField] public TrailRenderer TracerEffect { get; private set; }

    private float _rechargingTimer;
    private bool _isRecharging = false;

    private bool _isFiring;

    public void FunctioningTick()
    {
        if (_isRecharging)
        {
            Recharge();
        }
        else if (_isFiring)
        {
            FireShot();
        }
    }

    public void OpenFire()
    {
        if (_isFiring) return;

        _isFiring = true;
    }

    public void CeaseFire()
    {
        if (!_isFiring) return;

        _isFiring = false;
    }

    private void FireShot()
    {
        AudioSource.PlayOneShot(ShotSound);

        Ray shotRay = new Ray(Muzzle.position, Muzzle.forward);
        Vector3 impactPoint = CreateRaycast(shotRay);
        DrawTracer(Muzzle.position, Muzzle.rotation, impactPoint);

        _isRecharging = true;
    }

    private Vector3 CreateRaycast(Ray shotRay)
    {
        if (Physics.Raycast(shotRay, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out DroneHealth health))
            {
                health.SubtractHealth(Damage);
            }

            return hit.point;
        }
        else
        {
            return shotRay.GetPoint(1000);
        }
    }

    private void DrawTracer(Vector3 muzzlePoint, Quaternion direction, Vector3 impactPoint)
    {
        var tracer = UnityEngine.Object.Instantiate(TracerEffect, muzzlePoint, direction);
        tracer.AddPosition(muzzlePoint);

        tracer.transform.position = impactPoint;
    }

    private void Recharge()
    {
        _rechargingTimer += Time.deltaTime;

        if (_rechargingTimer >= 1 / RateOfFire)
        {
            _isRecharging = false;
            _rechargingTimer = 0;
        }
    }
}
