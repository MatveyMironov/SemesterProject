using System;
using UnityEngine;

public class LaserGun : Weapon
{
    private LaserGunComponents _components;
    private LaserGunParameters _parameters;

    public LaserGun(LaserGunComponents components, LaserGunParameters parameters) : base(parameters)
    {
        _components = components;
        _parameters = parameters;
    }

    public Transform Muzzle { get { return _components.Muzzle; } }

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

        _components.BeamEffect.enabled = true;
        _components.AudioSource.clip = _components.FiringSound;
        _components.AudioSource.Play();
    }

    public void CeaseFire()
    {
        if (!_isEmitting) 
            return;
        _isEmitting = false;

        _emittingTimer = 0;
        _needsRecharging = true;

        _components.BeamEffect.enabled = false;
        _components.AudioSource.Stop();
    }
    #endregion

    #region Beam
    private void EmitBeam()
    {
        Ray beamRay = new Ray(_components.Muzzle.position, _components.Muzzle.forward);
        Vector3 impactPoint = FireRaycast(beamRay);
        DrawBeam(impactPoint);

        _emittingTimer += Time.deltaTime;
        if (_emittingTimer > _parameters.MaxEmittingTime)
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
                _accumulatedDamage += _parameters.DamagePerSecond * Time.deltaTime;

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
        _components.BeamEffect.SetPosition(1, _components.BeamEffect.transform.InverseTransformPoint(impactPoint));
    }
    #endregion

    [Serializable]
    public class LaserGunComponents
    {
        [field: Header("Gun Parametres")]
        [field: SerializeField] public Transform Muzzle { get; private set; }

        [field: Header("Effects")]
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioClip FiringSound { get; private set; }
        [field: SerializeField] public LineRenderer BeamEffect { get; private set; }
    }

    [Serializable]
    public class LaserGunParameters : WeaponParameters
    {
        [field: Header("Beam")]
        [field: SerializeField] public int DamagePerSecond { get; private set; }
        [field: SerializeField] public float MaxEmittingTime { get; private set; }
    }
}
