using UnityEngine;

namespace ConstructionSystem
{
    public class Construction : MonoBehaviour
    {
        [SerializeField] private ResourceSystem.ResourceManager resourceManager;

        [Header("Construction Sites")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask constructionLayers;
        [SerializeField] private ConstructionSite[] constructionSites = new ConstructionSite[0];

        private TurretDataSO _selectedTurret;
        private Vector3 _previousMousePosition;
        private ConstructionSite _highlitedSite;

        private PreviewSystem _previewSystem = new();

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

        public void ShowConstructionSites()
        {
            foreach (var constructionSite in constructionSites)
            {
                if (constructionSite != null)
                {
                    constructionSite.ShowConstructionSite();
                }
            }
        }

        public void HideConstructionSites()
        {
            foreach (var constructionSite in constructionSites)
            {
                if (constructionSite != null)
                {
                    constructionSite.HideConstructionSite();
                }
            }
        }

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
                        BuildTurret(constructionSite, _selectedTurret);
                    }
                }
            }
        }

        private void BuildTurret(ConstructionSite constructionSite, TurretDataSO turretData)
        {
            if (!constructionSite.IsOccupied)
            {
                if (resourceManager.ResourceAmount >= turretData.TurretCost)
                {
                    resourceManager.SubtractResource(turretData.TurretCost);
                    constructionSite.Build(turretData.TurretPrefab);
                    AbortBuilding();
                }
                else
                {
                    Debug.Log("Not enough resource for that turret");
                }
            }
        }

        public void AbortBuilding()
        {
            _selectedTurret = null;
            _highlitedSite = null;
            _previewSystem.DestroyPreview();
        }
    }
}
