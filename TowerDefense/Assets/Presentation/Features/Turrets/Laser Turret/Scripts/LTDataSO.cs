using UnityEngine;

[CreateAssetMenu(fileName = "NewLaserTurretParameters", menuName = "Turrets Data/Laser Turret Parameters")]
public class LTDataSO : TurretDataSO
{
    [field: SerializeField] public LaserGun.LaserGunParameters LaserGunParameters { get; set; }
}
