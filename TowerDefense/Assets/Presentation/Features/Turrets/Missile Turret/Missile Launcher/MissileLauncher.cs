using System;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{
    private MissileLauncherComponents _components;
    private MissileLauncherParameters _parameters;

    private int _currentPodIndex = 0;

    public MissileLauncher(MissileLauncherComponents components, MissileLauncherParameters parameters) : base(parameters)
    {
        _components = components;
        _parameters = parameters;
    }

    public void LaunchMissile(Transform target)
    {
        if (!ReadyToFire)
            return;

        MissilePod missilePod = FindMissilePod();

        PlayLaunchEffects();

        Missile missile = UnityEngine.Object.Instantiate(_parameters.MissilePrefab, missilePod.MissileSpawn.position, missilePod.MissileSpawn.rotation);
        Missile.MissileProgram program = new(missilePod.MissileAttackPosition.position, target);
        missile.SetParameters(_parameters.MissileParameters);
        missile.Program(program);

        _needsRecharging = true;
    }

    private MissilePod FindMissilePod()
    {
        MissilePod missilePod = _components.MissilePods[_currentPodIndex];

        _currentPodIndex++;
        if (_currentPodIndex >= _components.MissilePods.Count)
        {
            _currentPodIndex = 0;
        }

        return missilePod;
    }

    private void PlayLaunchEffects()
    {

    }

    [Serializable]
    public class MissilePod
    {
        [field: SerializeField] public Transform MissileSpawn { get; private set; }
        [field: Tooltip("Rocket will go to attack position before starting to persue it's target")]
        [field: SerializeField] public Transform MissileAttackPosition { get; private set; }
        [field: SerializeField] public ParticleSystem MissileLaunchEffect { get; private set; }
    }

    [Serializable]
    public class MissileLauncherComponents
    {
        [field: Tooltip("Pods launch missiles in a cycle one after another")]
        [field: SerializeField] public List<MissilePod> MissilePods { get; private set; } = new();

        [field: Header("Effects")]
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioClip LaunchSound { get; private set; }
    }

    [Serializable]
    public class MissileLauncherParameters : WeaponParameters
    {
        [field: Header("Missile")]
        [field: SerializeField] public Missile MissilePrefab { get; private set; }
        [field: SerializeField] public Missile.MissileParameters MissileParameters { get; private set; }
    }
}
