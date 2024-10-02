using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private GameObject visuals;

    public bool IsOccupied { get; private set; }

    public void ShowConstructionSite()
    {
        if (IsOccupied)
            return;

        visuals.SetActive(true);
    }

    public void HideConstructionSite()
    {
        if (IsOccupied)
            return;

        visuals.SetActive(false);
    }

    public void Build(Turret turretPrefab)
    {
        if (IsOccupied)
            return;

        Turret turret = Instantiate(turretPrefab, transform.position, transform.rotation, transform);

        HideConstructionSite();

        IsOccupied = true;
    }
}
