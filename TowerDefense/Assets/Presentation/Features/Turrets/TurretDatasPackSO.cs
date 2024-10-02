using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTurretDatasPack", menuName = "Turrets Data/Turret Datas Pack")]
public class TurretDatasPackSO : ScriptableObject
{
    [field: SerializeField] public List<TurretDataSO> TurretDatas { get; private set; }
}
