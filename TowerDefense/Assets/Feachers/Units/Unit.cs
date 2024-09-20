using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum factions
    {
        Player = 0,
        Rival = 1,
    }

    [field: SerializeField] public factions Faction { get; private set; }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

}
