using System;
using UnityEngine;

[Serializable]
public class BeamGun
{
    [field: SerializeField] public Transform Muzzle { get; private set; }
    [field: SerializeField] public int DamagePerSecond { get; private set; }

    [field: Header("Effects")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [field: SerializeField] public AudioClip FiringSound { get; private set; }
    [field: SerializeField] public LineRenderer BeamEffect { get; private set; }

    private bool _isFiring;
    private float _accumulatedDamage;

    #region Fire Control
    public void FunctioningTick()
    {
        if (_isFiring)
        {
            FireBeam();
        }
    }

    public void OpenFire()
    {
        if (_isFiring) return;

        _isFiring = true;

        BeamEffect.enabled = true;

        AudioSource.clip = FiringSound;
        AudioSource.Play();
    }

    public void CeaseFire()
    {
        if (!_isFiring) return;

        _isFiring = false;

        BeamEffect.enabled = false;

        AudioSource.Stop();
    }
    #endregion

    private void FireBeam()
    {
        Ray beamRay = new Ray(Muzzle.position, Muzzle.forward);
        Vector3 impactPoint = FireRaycast(beamRay);
        DrawBeam(impactPoint);
    }

    private Vector3 FireRaycast(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Health health))
            {
                _accumulatedDamage += DamagePerSecond * Time.deltaTime;

                if (_accumulatedDamage >= 1)
                {
                    health.DealDamage(1);
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
        BeamEffect.SetPosition(1, BeamEffect.transform.InverseTransformPoint(impactPoint));
    }
}
