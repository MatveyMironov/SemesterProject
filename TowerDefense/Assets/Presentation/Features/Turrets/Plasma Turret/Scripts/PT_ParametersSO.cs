using UnityEngine;

[CreateAssetMenu(fileName = "NewPlasmaTurretParameters", menuName = "Turrets Parameters/Plasma Turret Parameters")]
public class PT_ParametersSO : TurretParametersSO
{
    [field: SerializeField] public PlasmaGun.PlasmaGunParameters PlasmaGunParameters { get; set; }
}
