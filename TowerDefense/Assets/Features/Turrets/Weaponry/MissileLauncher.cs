using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissilePod
{
    [field: SerializeField] public Transform MissileSpawn { get; private set; }
    [field: Tooltip("Rocket will go to attack position before startint to persue it's target")]
    [field: SerializeField] public Transform MissileAttackPosition { get; private set; }
    [field: SerializeField] public ParticleSystem MissileLaunchEffect { get; private set; }

    public bool IsMissileCharged = true;
}

[Serializable]
public class MissileLauncher
{
    [field: Tooltip("Each pod can launch one missile, than it has to be recharged")]
    [field: SerializeField] public List<MissilePod> MissilePods { get; private set; } = new List<MissilePod>();

    [field: Header("Launcher Characteristics")]
    [field: Tooltip("Min time between missile launches")]
    [field: SerializeField] public float LaunchCooldown { get; private set; }
    [field: SerializeField] public float MissilePodRechargeTime { get; private set; }

    [field: Header("Missile")]
    [field: SerializeField] public Missile missilePrefab { get; private set; }
    [field: SerializeField] public MissileCharacteristics missileCharateristics { get; private set; }

    [field: Header("Launch Effects")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [field: SerializeField] public AudioClip LaunchSound { get; private set; }

    private float _cooldownTimer;
    private bool _cooldownPassed = true;

    private List<MissilePod> _podsOnRecharge = new List<MissilePod>();
    private float _rechargingTimer;

    public bool ReadyToLaunch { get; private set; } = true;

    public void WorkingTick()
    {

    }

    public void FiringTick()
    {
        ReadyToLaunch = CheckIfReadyToLaunch();

        if (_podsOnRecharge.Count > 0)
        {
            RechargePod();
        }

        if (!_cooldownPassed)
        {
            LauncherCooldown();
        }
    }

    public void LaunchMissile(Transform target)
    {
        if (!ReadyToLaunch)
            return;

        if (FindChargedMissilePod(out MissilePod pod))
        {
            PlayLaunchEffects();

            Missile missile = UnityEngine.Object.Instantiate(missilePrefab, pod.MissileSpawn.position, pod.MissileSpawn.rotation);
            missile.MissileCharacteristics = missileCharateristics;
            
            MissileProgram program = new MissileProgram(pod.MissileAttackPosition.position, target);
            missile.ProgramMissile(program);

            _cooldownPassed = false;

            _podsOnRecharge.Add(pod);
        }
    }

    private bool FindChargedMissilePod(out MissilePod chargedPod)
    {
        foreach (MissilePod pod in MissilePods)
        {
            if (pod.IsMissileCharged)
            {
                chargedPod = pod;
                return true;
            }
        }

        chargedPod = null;
        return false;
    }

    private void PlayLaunchEffects()
    {

    }

    private void RechargePod()
    {
        _rechargingTimer += Time.deltaTime;
        if (_rechargingTimer >= MissilePodRechargeTime)
        {
            _podsOnRecharge[0].IsMissileCharged = true;
            _podsOnRecharge.RemoveAt(0);
            _rechargingTimer = 0;
        }
    }

    private void LauncherCooldown()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer >= LaunchCooldown)
        {
            _cooldownPassed = true;
            _cooldownTimer = 0;
        }
    }

    private bool CheckIfReadyToLaunch()
    {
        return FindChargedMissilePod(out MissilePod pod) && _cooldownPassed;
    }
}
