using UnityEngine;

public class PT_Fighting_State : IState
{
    private TargetDetection _detection;
    private TargetTracking _tracking;
    private PlasmaGun _plasmaGun;

    public PT_Fighting_State(TargetDetection targetDetection, TargetTracking targetTracking, PlasmaGun plasmaGun)
    {
        _detection = targetDetection;
        _tracking = targetTracking;
        _plasmaGun = plasmaGun;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        TrackTarget(_tracking, _detection.SelectedTarget);
        if (CheckAim(_plasmaGun, _detection))
        {
            _plasmaGun.LaunchPlasmoid(_detection.SelectedTarget.transform.position);
        }
    }

    private void TrackTarget(TargetTracking tracking, Drone target)
    {
        tracking.TrackTarget(target.transform);
    }

    private bool CheckAim(PlasmaGun plasmaGun, TargetDetection detection)
    {
        if (Physics.Raycast(plasmaGun.Muzzle.position, plasmaGun.Muzzle.forward, out RaycastHit hit))
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
