using UnityEngine;

public class SelectingTarget_State : IState
{
    private TargetDetection _detectionSystem;

    public SelectingTarget_State(TargetDetection detectionSystem)
    {
        _detectionSystem = detectionSystem;
    }

    public void OnEnter()
    {
        Debug.Log("Selecting target");

        if (_detectionSystem.AvailableTargets.Count > 0)
        {
            _detectionSystem.SelectedTarget = _detectionSystem.AvailableTargets[0];
        }
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
