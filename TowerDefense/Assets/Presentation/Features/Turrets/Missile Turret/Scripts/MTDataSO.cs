using UnityEngine;

[CreateAssetMenu(fileName = "NewMissileTurretParameters", menuName = "Turrets Data/Missile Turret Parameters")]
public class MTDataSO : TurretDataSO
{
    [field: SerializeField] public MissileLauncher.MissileLauncherParameters MissileLauncherParameters { get; set; }
}
