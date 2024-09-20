using UnityEngine;

[System.Serializable]
public class ConstructionPreview
{
    [SerializeField] private GameObject cellIndicator;

    private Renderer _cellIndicatorRenderer;

    [Space]
    [SerializeField] private Material previewMaterialPrefab;
    [SerializeField] private Color validPlacementColor;
    [SerializeField] private Color invalidPlacementColor;

    private Material _previewMaterialInstance;
    private GameObject _preview;

    private bool _isReady;

    public void PrepareConstructionPreview()
    {
        _previewMaterialInstance = new Material(previewMaterialPrefab);

        _cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        cellIndicator.gameObject.SetActive(false);

        _isReady = true;
    }

    public void ShowConstructionPreview(GameObject preview, Vector2Int size)
    {
        if (!_isReady) return;

        _preview = Object.Instantiate(preview);
        PreparePreview(_preview);
        PrepareCellIndicator(size);
        cellIndicator.SetActive(true);
    }

    public void HidePreview()
    {
        if (!_isReady) return;

        if (_preview != null)
        {
            Object.Destroy(_preview);
        }

        cellIndicator.SetActive(false);
    }

    public void ShowRemovalPreview()
    {
        PrepareCellIndicator(Vector2Int.one);
    }

    private void PreparePreview(GameObject preview)
    {
        if (!_isReady) return;

        Renderer[] renderers = preview.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = _previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    private void PrepareCellIndicator(Vector2Int size)
    {
        if (!_isReady) return;

        if (size.x > 0 && size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
        }
    }

    public void UpdatePreviewPosition(Vector3 position, bool placementValidity)
    {
        if (!_isReady) return;

        if (_preview != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(placementValidity);
        }

        MoveCellIndicator(position);
        ApplyFeedbackToCellIndicator(placementValidity);
    }

    private void MovePreview(Vector3 position)
    {
        _preview.transform.position = position;
    }

    private void MoveCellIndicator(Vector3 position)
    {
        cellIndicator.transform.position = new Vector3(position.x, position.y + 0.01f, position.z);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        _previewMaterialInstance.color = validity ? validPlacementColor : invalidPlacementColor;
    }

    private void ApplyFeedbackToCellIndicator(bool validity)
    {
        _cellIndicatorRenderer.material.color = validity ? validPlacementColor : invalidPlacementColor;
    }
}
