using System;
using UnityEngine;

[Serializable]
public class LaserGun
{
    [SerializeField] public Transform muzzle;
    [SerializeField] public int damagePerSecond;
    [SerializeField] private float maxEmittingTime;
    [SerializeField] private float rechargingTime;

    [Header("Effects")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip firingSound;
    [SerializeField] public LineRenderer beamEffect;

    private bool _isFiring;
    private float _emittingTimer;

    private bool _isRecharging;
    private float _rechargingTimer;

    private float _accumulatedDamage;

    #region Fire Control
    public void FunctioningTick()
    {
        if (_isRecharging)
        {
            Recharge();
        }
        else if (_isFiring)
        {
            EmitBeam();
        }
    }

    public void OpenFire()
    {
        if (_isFiring || _isRecharging) 
            return;
        _isFiring = true;

        beamEffect.enabled = true;
        audioSource.clip = firingSound;
        audioSource.Play();
    }

    public void CeaseFire()
    {
        if (!_isFiring) 
            return;
        _isFiring = false;

        _emittingTimer = 0;
        _isRecharging = true;

        beamEffect.enabled = false;
        audioSource.Stop();
    }
    #endregion

    #region Beam
    private void EmitBeam()
    {
        Ray beamRay = new Ray(muzzle.position, muzzle.forward);
        Vector3 impactPoint = FireRaycast(beamRay);
        DrawBeam(impactPoint);

        _emittingTimer += Time.deltaTime;
        if (_emittingTimer > maxEmittingTime)
        {
            CeaseFire();
        }
    }

    private Vector3 FireRaycast(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out DroneHealth health))
            {
                _accumulatedDamage += damagePerSecond * Time.deltaTime;

                if (_accumulatedDamage >= 1)
                {
                    health.SubtractHealth(1);
                    _accumulatedDamage -= 1;
                }
            }

            return hit.point;
        }
        else
        {
            return ray.GetPoint(1000);
        }
    }

    private void DrawBeam(Vector3 impactPoint)
    {
        beamEffect.SetPosition(1, beamEffect.transform.InverseTransformPoint(impactPoint));
    }
    #endregion

    private void Recharge()
    {
        _rechargingTimer += Time.deltaTime;
        if (_rechargingTimer >= rechargingTime)
        {
            _isRecharging = false;
            _rechargingTimer = 0;
        }
    }
}
