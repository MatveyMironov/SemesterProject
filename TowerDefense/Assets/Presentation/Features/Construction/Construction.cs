using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private ConstructionSite[] constructionSites = new ConstructionSite[0];

    [Header("Turrets")]
    [SerializeField] private LT_ParametersSO laserTurret;
    [SerializeField] private MT_ParametersSO missileTurret;
    [SerializeField] private PT_ParametersSO plasmaTurret;

    [Space]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask constructionLayers;

    [Space]
    [SerializeField] private ConstructionMenu constructionMenu;

    private bool _isConstructionModeEntered;

    private Turret _selectedTurretPrefab;

    private PreviewSystem _previewSystem = new();

    private void Start()
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

    #region Building
    public void SelectConstructionSite()
    {
        if (_selectedTurretPrefab == null)
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
                    BuildTurret(constructionSite, _selectedTurretPrefab);
                }
            }
        }
    }

    private void BuildTurret(ConstructionSite constructionSite, Turret turretPrefab)
    {
        if (!constructionSite.IsOccupied)
        {
            constructionSite.Build(turretPrefab);
            AbortBuilding();
        }
    }

    private void AbortBuilding()
    {
        _selectedTurretPrefab = null;
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
        _selectedTurretPrefab = laserTurret.TurretPrefab;
    }

    private void BuildMissileTurret()
    {
        _selectedTurretPrefab = missileTurret.TurretPrefab;
    }

    private void BuildPlasmaTurret()
    {
        _selectedTurretPrefab = plasmaTurret.TurretPrefab;
    }
}
