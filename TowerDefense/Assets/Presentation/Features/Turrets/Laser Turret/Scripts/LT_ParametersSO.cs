using UnityEngine;

[CreateAssetMenu(fileName = "NewLaserTurretParameters", menuName = "Turrets Parameters/Laser Turret Parameters")]
public class LT_ParametersSO : TurretDataSO
{
    [field: SerializeField] public LaserGun.LaserGunParameters LaserGunParameters { get; set; }
}
