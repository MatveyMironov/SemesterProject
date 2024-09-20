using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueprintsCollection", menuName = "Buildings/New Construction Blueprints Collection")]
public class ConstructionBlueprintsCollectionSO : ScriptableObject
{
    [field: SerializeField] public ConstructionBlueprintSO[] Blueprints = new ConstructionBlueprintSO[0];
}
