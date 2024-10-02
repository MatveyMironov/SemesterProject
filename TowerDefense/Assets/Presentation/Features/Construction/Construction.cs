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

    private TurretDataSO _selectedTurret;
    private Vector3 _previousMousePosition;
    private ConstructionSite _highlitedSite;

    private PreviewSystem _previewSystem = new();

    private void Start()
    {
        ExitConstructionMode();
    }

    private void Update()
    {
        if (_selectedTurret != null)
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition != _previousMousePosition)
            {
                mousePosition.z = mainCamera.nearClipPlane;

                Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000, constructionLayers))
                {
                    if (hit.collider.TryGetComponent(out ConstructionSite constructionSite))
                    {
                        if (!constructionSite.IsOccupied)
                        {
                            if (constructionSite != _highlitedSite)
                            {
                                _previewSystem.ShowPreview(constructionSite);
                                _highlitedSite = constructionSite;
                            }
                        }
                    }
                }
                else
                {
                    _previewSystem.HidePreview();
                    _highlitedSite = null;
                }

                _previousMousePosition = mousePosition;
            }
            
        }
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
    public void SelectTurret(TurretDataSO turret)
    {
        _selectedTurret = turret;
        _previewSystem.CreatePreview(turret.TurretPreview);
    }

    public void SelectConstructionSite()
    {
        if (_selectedTurret == null)
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
                    BuildTurret(constructionSite, _selectedTurret.TurretPrefab);
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
        _selectedTurret = null;
        _highlitedSite = null;
        _previewSystem.DestroyPreview();
    }
    #endregion

    private void SelectLaserTurret()
    {
        SelectTurret(laserTurret);
    }

    private void SelectMissileTurret()
    {
        SelectTurret(missileTurret);
    }

    private void SelectPlasmaTurret()
    {
        SelectTurret(plasmaTurret);
    }
}
