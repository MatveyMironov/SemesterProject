using System;
using UnityEngine;

[Serializable]
public class LaserGun : Weapon
{
    [SerializeField] public Transform muzzle;

    [Header("Beam")]
    [SerializeField] public int damagePerSecond;
    [SerializeField] private float maxEmittingTime;

    [Header("Effects")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip firingSound;
    [SerializeField] public LineRenderer beamEffect;

    private bool _isEmitting;
    private float _emittingTimer;

    private float _accumulatedDamage;

    #region Fire Control
    public override void FunctioningTick()
    {
        base.FunctioningTick();

        if (_isEmitting)
        {
            EmitBeam();
        }
    }

    public void OpenFire()
    {
        if (!ReadyToFire || _isEmitting) 
            return;

        _isEmitting = true;

        beamEffect.enabled = true;
        audioSource.clip = firingSound;
        audioSource.Play();
    }

    public void CeaseFire()
    {
        if (!_isEmitting) 
            return;
        _isEmitting = false;

        _emittingTimer = 0;
        _needsRecharging = true;

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
}
