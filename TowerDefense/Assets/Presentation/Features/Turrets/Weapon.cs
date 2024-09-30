using System;
using UnityEngine;

public abstract class Weapon
{
    private WeaponParameters _parameters;

    protected bool _needsRecharging = false;
    private float _rechargingTimer = 0;

    protected Weapon(WeaponParameters parameters)
    {
        _parameters = parameters;
    }

    public bool ReadyToFire { get; protected set; }

    public virtual void FunctioningTick()
    {
        ReadyToFire = CheckIfReadyToLaunch();

        if (_needsRecharging)
        {
            Recharge();
        }
    }

    private void Recharge()
    {
        _rechargingTimer += Time.deltaTime;
        if (_rechargingTimer >= _parameters.RechargingTime)
        {
            _needsRecharging = false;
            _rechargingTimer = 0;
        }
    }

    protected virtual bool CheckIfReadyToLaunch()
    {
        return (!_needsRecharging);
    }

    [Serializable] 
    public abstract class WeaponParameters
    {
        [field: Tooltip("Time between two individual shots/launches etc.")]
        [field: SerializeField] public float RechargingTime { get; private set; }
    }
}
