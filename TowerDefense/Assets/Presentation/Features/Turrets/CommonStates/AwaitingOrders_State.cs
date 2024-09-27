using UnityEngine;

public class AwaitingOrders_State : IState
{
    public AwaitingOrders_State()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Awaiting orders...");
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }
}
