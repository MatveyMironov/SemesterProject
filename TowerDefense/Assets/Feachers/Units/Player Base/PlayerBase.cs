using System;

public class PlayerBase : Unit
{
    public static event Action OnBaseDestroyed;

    public override void Die()
    {
        OnBaseDestroyed?.Invoke();

        base.Die();
    }
}
