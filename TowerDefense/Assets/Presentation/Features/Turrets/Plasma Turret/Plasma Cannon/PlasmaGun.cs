using System;
using UnityEngine;

[Serializable]
public class PlasmaGun : Weapon
{
    [Header("Gun Parametres")]
    [SerializeField] public Transform muzzle;

    [Header("Plasmoid")]
    [SerializeField] private Plasmoid plasmoidPrefab;
    [SerializeField] private Plasmoid.PlasmoidParameters plasmoidParameters;

    [Header("Effects")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip firingSound;

    public void LaunchPlasmoid(Vector3 point)
    {
        if (!ReadyToFire)
            return;

        Plasmoid plasmoid = UnityEngine.Object.Instantiate(plasmoidPrefab, muzzle.position, muzzle.rotation);
        float plasmoidExplosionTime = (point - muzzle.position).magnitude / plasmoidParameters.Speed;
        plasmoid.SetParameters(plasmoidParameters, plasmoidExplosionTime);

        _needsRecharging = true;
    }
}
