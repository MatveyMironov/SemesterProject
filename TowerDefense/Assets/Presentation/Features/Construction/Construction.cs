using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private ConstructionSite[] constructionSites = new ConstructionSite[0];
    private ConstructionSite _selectedSite;

    [Header("Turrets")]
    [SerializeField] private Turret laserTurretPrefab;
    [SerializeField] private Turret missileTurretPrefab;
    [SerializeField] private Turret plasmaTurretPrefab;

    [Space]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask constructionLayers;

    [Space]
    [SerializeField] private ConstructionMenu constructionMenu;

    private bool _isConstructionModeEntered;

    private void Awake()
    {
        ExitConstructionMode();
    }

    #region Construction Mode
    public void ToggleConstructionMode()
    {
        if (_isConstructionModeEntered)
        {
            ExitConstructionMode();
        }
        else
        {
            EnterConstructionMode();
        }
    }

    private void EnterConstructionMode()
    {
        foreach (var constructionSite in constructionSites)
        {
            if (constructionSite != null)
            {
                constructionSite.ShowConstructionSite();
            }
        }

        constructionMenu.OpenMenu();
        _isConstructionModeEntered = true;
    }

    private void ExitConstructionMode()
    {
        foreach (var constructionSite in constructionSites)
        {
            if (constructionSite != null)
            {
                constructionSite.HideConstructionSite();
            }
        }

        AbortBuilding();

        constructionMenu.CloseMenu();
        _isConstructionModeEntered = false;
    }
    #endregion

    public void SelectConstructionSite()
    {
        if (!_isConstructionModeEntered)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, constructionLayers))
        {
            if (hit.collider.TryGetComponent(out ConstructionSite constructionSite))
            {
                if (!constructionSite.IsOccupied)
                {
                    StartBuilding(constructionSite);
                }
            }
        }
    }

    #region Building
    private void StartBuilding(ConstructionSite constructionSite)
    {
        if (constructionSite == null)
            return;

        _selectedSite = constructionSite;
    }

    private void BuildTurret(ConstructionSite constructionSite, Turret turretPrefab)
    {
        if (!constructionSite.IsOccupied)
        {
            constructionSite.Build(turretPrefab);
        }
    }

    private void AbortBuilding()
    {
        _selectedSite = null;
    }
    #endregion

    private void OnEnable()
    {
        constructionMenu.OnLaserTurretClicked += BuildLaserTurret;
        constructionMenu.OnMissileTurretClicked += BuildMissileTurret;
        constructionMenu.OnPlasmaTurretClicked += BuildPlasmaTurret;
    }

    private void OnDisable()
    {
        constructionMenu.OnLaserTurretClicked -= BuildLaserTurret;
        constructionMenu.OnMissileTurretClicked -= BuildMissileTurret;
        constructionMenu.OnPlasmaTurretClicked -= BuildPlasmaTurret;
    }

    private void BuildLaserTurret()
    {
        BuildTurret(_selectedSite, laserTurretPrefab);
    }

    private void BuildMissileTurret()
    {
        BuildTurret(_selectedSite, missileTurretPrefab);
    }

    private void BuildPlasmaTurret()
    {
        BuildTurret(_selectedSite, plasmaTurretPrefab);
    }
}
