using System;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private MissileParameters _parameters;
    private MissileProgram _program;

    private bool _hasParameters = false;
    private bool _isActive = false;

    private bool _hasReachedAttackPosition;
    private float _lifeTimer = 0f;
    private float _forcedDetonationTime = 10.0f;

    public void SetParameters(MissileParameters parameters)
    {
        _parameters = parameters;
        _hasParameters = true;
    }

    public void Program(MissileProgram program)
    {
        _program = program;
        _isActive = true;
    }
    
    private void Update()
    {
        if (!_isActive || !_hasParameters)
            return;

        if (!_hasReachedAttackPosition)
        {
            MoveToPosition(_program.AttackPosition);
            _hasReachedAttackPosition = transform.position == _program.AttackPosition;
            return;
        }

        if (_program.Target != null)
        {
            MoveToPosition(_program.Target.position);
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, _parameters.AngularSpeed * Time.deltaTime);
            transform.position += transform.forward * _parameters.MovementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, _parameters.MovementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive && _hasParameters)
        {
            if (collision.transform.TryGetComponent(out DroneHealth health))
            {
                health.SubtractHealth(_parameters.Damage);
            }

            Detonate();
        }
    }

    private void Detonate()
    {
        Instantiate(_parameters.ExplosionPrefab, transform.position, Quaternion.identity);

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

    [Serializable]
    public struct MissileParameters
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

}
