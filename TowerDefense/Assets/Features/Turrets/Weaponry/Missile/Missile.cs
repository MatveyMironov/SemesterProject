using System;
using UnityEngine;

[Serializable]
public struct MissileCharacteristics
{
    [field: Header("Combat Characteristics")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: Tooltip("Degrees per second")]
    [field: SerializeField] public int AngularSpeed { get; private set; }
    [field: SerializeField] public int ExplosionRadius { get; private set; }
    [field: SerializeField] public int ExplosionDamage { get; private set; }

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
    private Rigidbody _rigidbody;

    [field: SerializeField] public MissileCharacteristics MissileCharacteristics { get; set; }
    [field: SerializeField] public MissileProgram MissileProgram { get; private set; }

    [field: Space]
    [field: SerializeField] private bool isActive;

    private bool _isSensorActive;
    private bool _hasReachedAttackPosition;
    private float _lifeTimer = 0f;
    private float _sensorActivationTime = 0.5f;
    private float _forcedDetonationTime = 10.0f;

    public void ProgramMissile(MissileProgram program)
    {
        MissileProgram = program;
        isActive = true;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (MissileProgram.Target != null)
        {
            MoveToPosition(MissileProgram.Target.position);
        }
        else
        {
            Detonate();
        }

        CountTimer();
    }

    private void MoveToPosition(Vector3 destination)
    {
        Vector3 lookDirection = destination - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        if (transform.rotation != lookRotation)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRotation, MissileCharacteristics.AngularSpeed * Time.deltaTime));
        }

        _rigidbody.MovePosition(_rigidbody.position + transform.forward * MissileCharacteristics.MovementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (isActive && _isSensorActive)
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        Instantiate(MissileCharacteristics.ExplosionPrefab, transform.position, Quaternion.identity);

        Collider[] strickenObjects = Physics.OverlapSphere(transform.position, MissileCharacteristics.ExplosionRadius);

        foreach (Collider collider in strickenObjects)
        {
            if (collider.TryGetComponent(out Health health))
            {
                health.DealDamage(MissileCharacteristics.ExplosionDamage);
            }
        }

        Destroy(gameObject);
    }

    private void CountTimer()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _sensorActivationTime)
        {
            _isSensorActive = true;
        }

        if (_lifeTimer >= _forcedDetonationTime)
        {
            Detonate();
        }
    }
}
