using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueprint", menuName = "Buildings/New Construction Blueprint")]
public class ConstructionBlueprintSO : ScriptableObject
{
    [field: SerializeField] public string BuildingName { get; private set; }
    [field: SerializeField] public Vector2Int BuildingSize { get; private set; }
    [field: SerializeField] public Building Building { get; private set; }
    [field: SerializeField] public Building BuildingPreview { get; private set; }
    [field: SerializeField] public int BuildingCost { get; private set; }
}
