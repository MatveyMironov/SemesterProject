using UnityEngine;

public class PlacingState : IConstructionState
{
    private Grid _grid;
    private ConstructionPreview _constructionPreview;
    private ConstructionBlueprintSO _blueprint;
    private GridData _buildingsData;
    Resource _resource;

    public PlacingState(Grid grid,
                          ConstructionPreview constructionPreview,
                          ConstructionBlueprintSO blueprint,
                          GridData buildingsData,
                          Resource resource)
    {
        _grid = grid;
        _constructionPreview = constructionPreview;
        _blueprint = blueprint;
        _buildingsData = buildingsData;
        _resource = resource;

        _constructionPreview.ShowConstructionPreview(blueprint.BuildingPreview.gameObject, blueprint.BuildingSize);
    }

    public void EndState()
    {
        _constructionPreview.HidePreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        if (!CheckPlacementValidity(gridPosition, _blueprint))
        {
            return;
        }

        if (_resource.SubtractResource(_blueprint.BuildingCost))
        {
            Building building = Object.Instantiate(_blueprint.Building);
            building.transform.position = gridPosition;

            _buildingsData.AddObject(gridPosition, _blueprint.BuildingSize, building);

            _constructionPreview.UpdatePreviewPosition(_grid.CellToWorld(gridPosition), false);
        }
        else
        {
            Debug.Log("Not enough resource!");
        }
    }

    public bool CheckPlacementValidity(Vector3Int gridPosition, ConstructionBlueprintSO blueprint)
    {
        return _buildingsData.CanPlaceObjectAt(gridPosition, blueprint.BuildingSize);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, _blueprint);

        _constructionPreview.UpdatePreviewPosition(_grid.CellToWorld(gridPosition), placementValidity);
    }
}
