using UnityEngine;

[CreateAssetMenu(fileName = "NewMissileTurretParameters", menuName = "Turrets Parameters/Missile Turret Parameters")]
public class MT_ParametersSO : TurretParametersSO
{
    [field: SerializeField] public MissileLauncher.MissileLauncherParameters MissileLauncherParameters { get; set; }
}
