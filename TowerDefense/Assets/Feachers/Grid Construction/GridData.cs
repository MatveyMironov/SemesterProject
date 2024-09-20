using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector3Int, PlacementData> _placedObjects = new();

    public void AddObject(Vector3Int gridPosition, Vector2Int objectSize, Building building)
    {
        List<Vector3Int> positionsToCcupy = CalculatePositions(gridPosition, objectSize);

        PlacementData placementData = new PlacementData(positionsToCcupy, building);

        foreach (var position in positionsToCcupy)
        {
            if (_placedObjects.ContainsKey(position))
            {
                throw new Exception($"Dictionary already contains this cell position {position}");
            }

            _placedObjects[position] = placementData;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positions = new();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                positions.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return positions;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var position in positionsToOccupy)
        {
            if (_placedObjects.ContainsKey(position))
            {
                return false;
            }
        }
        return true;
    }

    public Building GetBuilding(Vector3Int gridPosition)
    {
        if (!_placedObjects.ContainsKey(gridPosition))
            return null;

        return _placedObjects[gridPosition].Building;
    }

    public void RemoveBuilding(Vector3Int gridPosition)
    {
        foreach (var position in _placedObjects[gridPosition].OccupiedCells)
        {
            _placedObjects.Remove(position);
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> OccupiedCells { get; private set; }
    public Building Building { get; private set; }

    public PlacementData(List<Vector3Int> occupiedCells, Building building)
    {
        OccupiedCells = occupiedCells;
        Building = building;
    }
}
