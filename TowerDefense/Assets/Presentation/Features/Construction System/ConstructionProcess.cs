using UnityEngine;

namespace ConstructionSystem
{
    public class ConstructionProcess : MonoBehaviour
    {
        [Header("Construction Sites")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask constructionLayers;

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

        public void SelectTurret(TurretDataSO turret)
        {
            _selectedTurret = turret;
            _previewSystem.CreatePreview(turret.TurretPreview);
        }

        public void SelectConstructionSite()
        {
            if (_selectedTurret == null)
                return;

            if (!_highlitedSite.IsOccupied)
            {
                ConstructTurret(_highlitedSite, _selectedTurret.TurretPrefab);
            }
        }

        private void ConstructTurret(ConstructionSite constructionSite, Turret turretPrefab)
        {
            if (!constructionSite.IsOccupied)
            {
                constructionSite.Build(turretPrefab);
                AbortConstruction();
            }
        }

        public void AbortConstruction()
        {
            _selectedTurret = null;
            _highlitedSite = null;
            _previewSystem.DestroyPreview();
        }
    }
}
