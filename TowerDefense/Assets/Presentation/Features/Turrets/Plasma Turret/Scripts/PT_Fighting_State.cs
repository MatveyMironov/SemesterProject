using UnityEngine;

public class PT_Fighting_State : IState
{
    private TargetDetection _detectionSystem;
    private TargetTracking _targetTracking;
    private PlasmaGun _plasmaGun;

    public PT_Fighting_State(TargetDetection detectionSystem, TargetTracking targetTracking, PlasmaGun plasmaGun)
    {
        _detectionSystem = detectionSystem;
        _targetTracking = targetTracking;
        _plasmaGun = plasmaGun;
    }

    public void OnEnter()
    {
        Debug.Log("Executing combat algorythm");
    }

    public void OnExit()
    {
        Debug.Log("Target lost");
    }

    public void Tick()
    {
        TrackTarget(_targetTracking, _detectionSystem.SelectedTarget);
        if (CheckAim(_plasmaGun, _detectionSystem))
        {
            _plasmaGun.LaunchPlasmoid(_detectionSystem.SelectedTarget.transform.position);
        }
    }

    private void TrackTarget(TargetTracking targetTracking, Drone target)
    {
        targetTracking.TrackTarget(target.transform);
    }

    private bool CheckAim(PlasmaGun plasmaGun, TargetDetection detectionSystem)
    {
        if (Physics.Raycast(plasmaGun.muzzle.position, plasmaGun.muzzle.forward, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Drone unit))
            {
                if (unit == detectionSystem.SelectedTarget)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
