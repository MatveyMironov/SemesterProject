public class MT_Fighting_State : IState
{
    private TargetDetection _detection;
    private TargetTracking _tracking;
    private MissileLauncher _missileLauncher;

    public MT_Fighting_State(TargetDetection detection, TargetTracking tracking, MissileLauncher missileLauncher)
    {
        _detection = detection;
        _tracking = tracking;
        _missileLauncher = missileLauncher;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        _tracking.TrackTarget(_detection.SelectedTarget.transform);

        _missileLauncher.FunctioningTick();

        if (_missileLauncher.ReadyToLaunch)
        {
            _missileLauncher.LaunchMissile(_detection.SelectedTarget.transform);
        }
    }
}
