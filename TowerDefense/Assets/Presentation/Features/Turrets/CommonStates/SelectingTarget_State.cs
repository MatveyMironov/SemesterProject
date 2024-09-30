public class SelectingTarget_State : IState
{
    private TargetDetection _detection;

    public SelectingTarget_State(TargetDetection detection)
    {
        _detection = detection;
    }

    public void OnEnter()
    {
        if (_detection.AvailableTargets.Count > 0)
        {
            _detection.SelectedTarget = _detection.AvailableTargets[0];
        }
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
