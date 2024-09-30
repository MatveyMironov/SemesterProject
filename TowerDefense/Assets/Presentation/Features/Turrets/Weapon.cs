using UnityEngine;

public abstract class Weapon
{
    [Tooltip("Time between two individual shots/launches etc.")]
    [SerializeField] private float rechargingTime;

    public bool ReadyToFire { get; protected set; }

    protected bool _needsRecharging = false;
    private float _rechargingTimer;

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
        if (_rechargingTimer >= rechargingTime)
        {
            _needsRecharging = false;
            _rechargingTimer = 0;
        }
    }

    protected virtual bool CheckIfReadyToLaunch()
    {
        return (!_needsRecharging);
    }
}
