using UnityEngine;

public class MT_Fighting_State : IState
{
    private TargetDetection _detectionSystem;
    private TargetTracking _targetTracking;
    private MissileLauncher _missileLauncher;

    public MT_Fighting_State(TargetDetection detetctionsystem, TargetTracking targetTracking, MissileLauncher missileLauncher)
    {
        _detectionSystem = detetctionsystem;
        _targetTracking = targetTracking;
        _missileLauncher = missileLauncher;
    }

    public void OnEnter()
    {
        Debug.Log("Eliminate target");
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        _targetTracking.TrackTarget(_detectionSystem.SelectedTarget.transform);

        _missileLauncher.FunctioningTick();

        if (_missileLauncher.ReadyToLaunch)
        {
            _missileLauncher.LaunchMissile(_detectionSystem.SelectedTarget.transform);
        }
    }
}
