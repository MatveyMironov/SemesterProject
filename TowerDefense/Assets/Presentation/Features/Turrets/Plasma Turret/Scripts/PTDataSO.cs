using UnityEngine;

[CreateAssetMenu(fileName = "NewPlasmaTurretParameters", menuName = "Turrets Data/Plasma Turret Parameters")]
public class PTDataSO : TurretDataSO
{
    [field: SerializeField] public PlasmaGun.PlasmaGunParameters PlasmaGunParameters { get; set; }
}
