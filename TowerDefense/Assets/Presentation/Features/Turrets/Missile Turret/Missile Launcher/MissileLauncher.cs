using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileLauncher
{
    [Tooltip("Pods launch missiles in a cycle one after another")]
    [SerializeField] private List<MissilePod> missilePods = new();

    [Header("Launcher")]
    [Tooltip("Time between two individual missiles can be launched")]
    [SerializeField] private float launchCooldown;

    [Header("Missile")]
    [SerializeField] private Missile missilePrefab;
    [SerializeField] private Missile.MissileParameters missileParameters;

    [Header("Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip launchSound;

    private int _currentPodIndex = 0;

    private bool _isOnCooldown = false;
    private float _cooldownTimer;

    public bool ReadyToLaunch { get; private set; }

    public void FunctioningTick()
    {
        ReadyToLaunch = CheckIfReadyToLaunch();

        if (_isOnCooldown)
        {
            LauncherCooldown();
        }
    }

    public void LaunchMissile(Transform target)
    {
        if (!ReadyToLaunch)
            return;

        MissilePod missilePod = FindMissilePod();

        PlayLaunchEffects();

        Missile missile = UnityEngine.Object.Instantiate(missilePrefab, missilePod.MissileSpawn.position, missilePod.MissileSpawn.rotation);
        Missile.MissileProgram program = new(missilePod.MissileAttackPosition.position, target);
        missile.SetParameters(missileParameters);
        missile.Program(program);

        _isOnCooldown = true;

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

    private void LauncherCooldown()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer >= launchCooldown)
        {
            _isOnCooldown = false;
            _cooldownTimer = 0;
        }
    }

    private bool CheckIfReadyToLaunch()
    {
        return !(_isOnCooldown);
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
