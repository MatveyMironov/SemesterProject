using System;
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

    private bool _isConstructionModeEntered;

    public static event Action OnConstructionModeEntered;
    public static event Action OnConstructionModeExited;

    private void Awake()
    {
        ExitConstructionMode();
    }

    #region Construction Mode
    private void ToggleConstructionMode()
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

        OnConstructionModeEntered?.Invoke();
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

        OnConstructionModeExited?.Invoke();
        _isConstructionModeEntered = false;
    }
    #endregion

    private void SelectConstructionSite()
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
        InputListener.OnConstructionToggled += ToggleConstructionMode;
        InputListener.OnSelected += SelectConstructionSite;

        ConstructionMenu.OnLaserTurretClicked += BuildLaserTurret;
        ConstructionMenu.OnMissileTurretClicked += BuildMissileTurret;
        ConstructionMenu.OnPlasmaTurretClicked += BuildPlasmaTurret;
    }

    private void OnDisable()
    {
        InputListener.OnConstructionToggled -= ToggleConstructionMode;
        InputListener.OnSelected -= SelectConstructionSite;

        ConstructionMenu.OnLaserTurretClicked -= BuildLaserTurret;
        ConstructionMenu.OnMissileTurretClicked -= BuildMissileTurret;
        ConstructionMenu.OnPlasmaTurretClicked -= BuildPlasmaTurret;
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
