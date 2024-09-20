using UnityEngine;

public class MachinegunTurretFighting_State : IState
{
    private DetectionSystem _detectionSystem;
    private TargetTracking _targetTracking;
    private MachineGun _machineGun;

    public MachinegunTurretFighting_State(DetectionSystem detectionSystem, TargetTracking targetTracking, MachineGun machineGun)
    {
        _detectionSystem = detectionSystem;
        _targetTracking = targetTracking;
        _machineGun = machineGun;
    }

    public void OnEnter()
    {
        Debug.Log("Executing combat algorythm");
    }

    public void OnExit()
    {
        Debug.Log("Target lost");
        _machineGun.CeaseFire();
    }

    public void Tick()
    {
        TrackTarget(_targetTracking, _detectionSystem.SelectedTarget);
        if (CheckAim(_machineGun, _detectionSystem))
        {
            _machineGun.OpenFire();
        }
        else
        {
            _machineGun.CeaseFire();
        }
    }

    private void TrackTarget(TargetTracking targetTracking, Unit target)
    {
        targetTracking.TrackTarget(target.transform);
    }

    private bool CheckAim(MachineGun machineGun, DetectionSystem detectionSystem)
    {
        if (Physics.Raycast(machineGun.Muzzle.position, machineGun.Muzzle.forward, out RaycastHit hit))
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
