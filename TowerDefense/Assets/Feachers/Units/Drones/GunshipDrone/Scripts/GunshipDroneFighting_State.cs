using UnityEngine;
using UnityEngine.AI;

public class GunshipDroneFighting_State : IState
{
    private DetectionSystem _detectionSystem;
    private DroneMovement _droneMovement;
    private TargetTracking _targetTracking;
    private MachineGun _machineGun;

    public GunshipDroneFighting_State(DetectionSystem detectionSystem, DroneMovement droneMovement, TargetTracking targetTracking, MachineGun machineGun)
    {
        _detectionSystem = detectionSystem;
        _droneMovement = droneMovement;
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
        PersueTarget(_droneMovement.Agent, _detectionSystem.SelectedTarget.transform, _detectionSystem.DetectionRadius - 1);
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

    private void PersueTarget(NavMeshAgent agent, Transform target, float desiredDistance)
    {
        agent.SetDestination(target.position);

        if (agent.remainingDistance <= desiredDistance)
        {
            agent.isStopped = true;
        }
        else if (agent.isStopped)
        {
            agent.isStopped = false;
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
