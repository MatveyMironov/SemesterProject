using UnityEngine;

public class LaserTurretFighting_State : IState
{
    private DetectionSystem _detectionSystem;
    private TargetTracking _targetTracking;
    private BeamGun _beamGun;

    public LaserTurretFighting_State(DetectionSystem detectionSystem, TargetTracking targetTracking, BeamGun beamGun)
    {
        _detectionSystem = detectionSystem;
        _targetTracking = targetTracking;
        _beamGun = beamGun;
    }

    public void OnEnter()
    {
        Debug.Log("Executing combat algorythm");
    }

    public void OnExit()
    {
        Debug.Log("Target lost");
        _beamGun.CeaseFire();
    }

    public void Tick()
    {
        TrackTarget(_targetTracking, _detectionSystem.SelectedTarget);
        if (CheckAim(_beamGun, _detectionSystem))
        {
            _beamGun.OpenFire();
        }
        else
        {
            _beamGun.CeaseFire();
        }
    }

    private void TrackTarget(TargetTracking targetTracking, Unit target)
    {
        targetTracking.TrackTarget(target.transform);
    }

    private bool CheckAim(BeamGun beamGun, DetectionSystem detectionSystem)
    {
        if (Physics.Raycast(beamGun.Muzzle.position, beamGun.Muzzle.forward, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Unit unit))
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
