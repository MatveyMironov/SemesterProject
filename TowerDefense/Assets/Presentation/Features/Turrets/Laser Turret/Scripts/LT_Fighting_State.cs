using UnityEngine;

public class LT_Fighting_State : IState
{
    private TargetDetection _detection;
    private TargetTracking _tracking;
    private LaserGun _laserGun;

    public LT_Fighting_State(TargetDetection detection, TargetTracking tracking, LaserGun laserGun)
    {
        _detection = detection;
        _tracking = tracking;
        _laserGun = laserGun;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _laserGun.CeaseFire();
    }

    public void Tick()
    {
        TrackTarget(_tracking, _detection.SelectedTarget);
        if (CheckAim(_laserGun, _detection))
        {
            _laserGun.OpenFire();
        }
        else
        {
            _laserGun.CeaseFire();
        }
    }

    private void TrackTarget(TargetTracking tracking, Drone target)
    {
        tracking.TrackTarget(target.transform);
    }

    private bool CheckAim(LaserGun laserGun, TargetDetection detection)
    {
        if (Physics.Raycast(laserGun.muzzle.position, laserGun.muzzle.forward, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Drone unit))
            {
                if (unit == detection.SelectedTarget)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
