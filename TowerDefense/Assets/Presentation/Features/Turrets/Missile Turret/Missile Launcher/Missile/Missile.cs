using System;
using UnityEngine;

[Serializable]
public struct MissileCharacteristics
{
    [field: Header("Combat Characteristics")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: Tooltip("Degrees per second")]
    [field: SerializeField] public int AngularSpeed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    [field: Header("Effects")]
    [field: SerializeField] public AudioClip FlightSound { get; private set; }
    [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
}

[Serializable]
public struct MissileProgram
{
    public MissileProgram(Vector3 attackPosition, Transform target)
    {
        AttackPosition = attackPosition;
        Target = target;
    }

    [field: Header("Combat Objectives")]
    [field: SerializeField] public Vector3 AttackPosition { get; private set; }
    [field: SerializeField] public Transform Target { get; private set; }
}

public class Missile : MonoBehaviour
{
    [field: SerializeField] public MissileCharacteristics MissileCharacteristics { get; set; }
    [field: SerializeField] public MissileProgram MissileProgram { get; private set; }

    [Space]
    [SerializeField] private bool isActive;

    private bool _hasReachedAttackPosition;
    private float _lifeTimer = 0f;
    private float _forcedDetonationTime = 10.0f;

    public void ProgramMissile(MissileProgram program)
    {
        MissileProgram = program;
        isActive = true;
    }
    
    private void Update()
    {
        if (!isActive)
            return;

        if (!_hasReachedAttackPosition)
        {
            MoveToPosition(MissileProgram.AttackPosition);
            _hasReachedAttackPosition = transform.position == MissileProgram.AttackPosition;
            return;
        }

        if (MissileProgram.Target != null)
        {
            MoveToPosition(MissileProgram.Target.position);
            CountLifeTime();
        }
        else
        {
            Detonate();
        }
    }

    private void MoveToPosition(Vector3 destination)
    {
        Vector3 lookDirection = destination - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        if (transform.rotation != lookRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, MissileCharacteristics.AngularSpeed * Time.deltaTime);
            transform.position += transform.forward * MissileCharacteristics.MovementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, MissileCharacteristics.MovementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            if (collision.transform.TryGetComponent(out DroneHealth health))
            {
                health.SubtractHealth(MissileCharacteristics.Damage);
            }

            Detonate();
        }
    }

    private void Detonate()
    {
        Instantiate(MissileCharacteristics.ExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void CountLifeTime()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _forcedDetonationTime)
        {
            Detonate();
        }
    }
}
