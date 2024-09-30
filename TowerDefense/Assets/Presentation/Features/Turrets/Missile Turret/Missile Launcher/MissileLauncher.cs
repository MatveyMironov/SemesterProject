using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileLauncher : Weapon
{
    [Tooltip("Pods launch missiles in a cycle one after another")]
    [SerializeField] private List<MissilePod> missilePods = new();

    [Header("Missile")]
    [SerializeField] private Missile missilePrefab;
    [SerializeField] private Missile.MissileParameters missileParameters;

    [Header("Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip launchSound;

    private int _currentPodIndex = 0;

    public void LaunchMissile(Transform target)
    {
        if (!ReadyToFire)
            return;

        MissilePod missilePod = FindMissilePod();

        PlayLaunchEffects();

        Missile missile = UnityEngine.Object.Instantiate(missilePrefab, missilePod.MissileSpawn.position, missilePod.MissileSpawn.rotation);
        Missile.MissileProgram program = new(missilePod.MissileAttackPosition.position, target);
        missile.SetParameters(missileParameters);
        missile.Program(program);

        _needsRecharging = true;
    }

    private MissilePod FindMissilePod()
    {
        MissilePod missilePod = missilePods[_currentPodIndex];

        _currentPodIndex++;
        if (_currentPodIndex >= missilePods.Count)
        {
            _currentPodIndex = 0;
        }

        return missilePod;
    }

    private void PlayLaunchEffects()
    {

    }

    [Serializable]
    private class MissilePod
    {
        [field: SerializeField] public Transform MissileSpawn { get; private set; }
        [field: Tooltip("Rocket will go to attack position before starting to persue it's target")]
        [field: SerializeField] public Transform MissileAttackPosition { get; private set; }
        [field: SerializeField] public ParticleSystem MissileLaunchEffect { get; private set; }
    }
}
