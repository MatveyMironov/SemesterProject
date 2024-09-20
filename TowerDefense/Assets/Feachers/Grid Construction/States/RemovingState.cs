using UnityEngine;

public class RemovingState : IConstructionState
{
    private ConstructionPreview _constructionPreview;
    private GridData _buildingsData;
    private BuildingRemovement _buildingRemovement;

    public RemovingState(ConstructionPreview constructionPreview,
                          GridData buildingsData,
                          BuildingRemovement buildingRemovement)
    {
        _constructionPreview = constructionPreview;
        _buildingsData = buildingsData;
        _buildingRemovement = buildingRemovement;
    }

    public void EndState()
    {
        _constructionPreview.HidePreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;

        if (CheckIfSelectionIsValid(gridPosition))
        {
            selectedData = _buildingsData;
        }

        if (selectedData == null)
        {

        }
        else
        {
            Building building = selectedData.GetBuilding(gridPosition);
            selectedData.RemoveBuilding(gridPosition);
            _buildingRemovement.RemoveBuilding(building);
        }
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !_buildingsData.CanPlaceObjectAt(gridPosition, Vector2Int.one);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        _constructionPreview.UpdatePreviewPosition(gridPosition, validity);
    }
}
