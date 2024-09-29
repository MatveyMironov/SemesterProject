using System;
using UnityEngine;

[Serializable]
public class PlasmaGun
{
    [Header("Gun Parametres")]
    [SerializeField] public Transform muzzle;
    [SerializeField] private float rechargingTime;

    [Header("Plasmoid")]
    [SerializeField] private Plasmoid plasmoidPrefab;
    [SerializeField] private Plasmoid.PlasmoidParameters plasmoidParameters;

    [Header("Effects")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip firingSound;

    private bool _isRecharging;
    private float _rechargingTimer;

    public void FunctioningTick()
    {
        if (_isRecharging)
        {
            Recharge();
        }
    }

    public void LaunchPlasmoid(Vector3 point)
    {
        if (_isRecharging)
            return;

        Plasmoid plasmoid = UnityEngine.Object.Instantiate(plasmoidPrefab, muzzle.position, muzzle.rotation);
        float plasmoidExplosionTime = (point - muzzle.position).magnitude / plasmoidParameters.Speed;
        plasmoid.SetParameters(plasmoidParameters, plasmoidExplosionTime);

        _isRecharging = true;
    }

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
