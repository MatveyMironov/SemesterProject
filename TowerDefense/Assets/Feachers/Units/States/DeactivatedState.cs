using UnityEngine;

public class DeactivatedState : IState
{
    public DeactivatedState()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Sleeping mode...");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
