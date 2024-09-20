using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMissileLauncher_State : IState
{
    private DetectionSystem _detectionSystem;
    private TargetTracking _targetTracking;
    private MissileLauncher _missileLauncher;

    public FiringMissileLauncher_State(DetectionSystem detetctionsystem, TargetTracking targetTracking, MissileLauncher missileLauncher)
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

        _missileLauncher.FiringTick();

        if (_missileLauncher.ReadyToLaunch)
        {
            _missileLauncher.LaunchMissile(_detectionSystem.SelectedTarget.transform);
        }
    }
}
