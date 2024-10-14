using System;
using UnityEngine;

public class PlasmaGun : Weapon
{
    private PlasmaGunComponents _components;
    private PlasmaGunParameters _parameters;

    public PlasmaGun(PlasmaGunComponents components, PlasmaGunParameters parameters) : base(parameters)
    {
        _components = components;
        _parameters = parameters;
    }

    public Transform Muzzle { get { return _components.Muzzle; } }

    public void LaunchPlasmoid(Vector3 point)
    {
        if (!ReadyToFire)
            return;

        Plasmoid plasmoid = UnityEngine.Object.Instantiate(_parameters.PlasmoidPrefab, _components.Muzzle.position, _components.Muzzle.rotation);
        float plasmoidExplosionTime = (point - _components.Muzzle.position).magnitude / _parameters.PlasmoidParameters.ProjectileParameters.Speed;
        plasmoid.SetParameters(_parameters.PlasmoidParameters, plasmoidExplosionTime);

        _needsRecharging = true;
    }

    [Serializable]
    public class PlasmaGunComponents
    {
        [field: Header("Gun Parametres")]
        [field: SerializeField] public Transform Muzzle { get; private set; }

        [field: Header("Effects")]
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioClip FiringSound { get; private set; }
    }

    [Serializable]
    public class PlasmaGunParameters : WeaponParameters
    {
        [field: Header("Plasmoid")]
        [field: SerializeField] public Plasmoid PlasmoidPrefab { get; private set; }
        [field: SerializeField] public Plasmoid.PlasmoidParameters PlasmoidParameters { get; private set; }
    }
}
