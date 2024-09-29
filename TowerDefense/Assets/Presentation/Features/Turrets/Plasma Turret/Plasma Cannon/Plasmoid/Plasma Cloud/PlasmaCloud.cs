using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlasmaCloud : MonoBehaviour
{
    private SphereCollider _collider;

    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _mainModule;
    private ParticleSystem.ShapeModule _shapeModule;

    private float _decayTime;
    private int _damagePerSecond;
    private bool _hasParameters = false;

    private float _lifeTime;

    private List<AffectedDrone> _affectedDrones = new();

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        if (TryGetComponent(out _particleSystem))
        {
            _mainModule = _particleSystem.main;
            _shapeModule = _particleSystem.shape;
        }
    }

    private void Update()
    {
        if (!_hasParameters)
            return;

        DealDamageToAffected();
        CountLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasParameters)
            return;

        if (other.TryGetComponent(out Drone drone))
        {
            AffectedDrone affectedDrone = new AffectedDrone(drone);
            _affectedDrones.Add(affectedDrone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_hasParameters)
            return;

        if (other.TryGetComponent(out Drone drone))
        {
            _affectedDrones.Remove(_affectedDrones.SingleOrDefault(affectedDrone => affectedDrone.Drone == drone));
        }
    }

    public void SetParametres(float radius, int damagePerSecond, float decayTime)
    {
        if (_hasParameters)
            return;

        _collider.radius = radius;
        if (_particleSystem != null)
        {
            _mainModule.startSizeMultiplier = radius;
            _shapeModule.radius = radius * 0.5f;
        }

        _decayTime = decayTime;
        _damagePerSecond = damagePerSecond;
        _hasParameters = true;
    }

    private void DealDamageToAffected()
    {
        for (int i = _affectedDrones.Count - 1; i >= 0; i--)
        {
            AffectedDrone affectedDrone = _affectedDrones[i];
            if (affectedDrone.Drone == null)
            {
                _affectedDrones.RemoveAt(i);
                continue;
            }

            affectedDrone.AccumulatedDamage += _damagePerSecond * Time.deltaTime;
            if (affectedDrone.AccumulatedDamage >= 1)
            {
                affectedDrone.Drone.transform.GetComponent<DroneHealth>().SubtractHealth(1);
                affectedDrone.AccumulatedDamage -= 1;
            }
        }
    }

    private void CountLifeTime()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime >= _decayTime)
        {
            Destroy(gameObject);
        }
    }

    private class AffectedDrone
    {
        public Drone Drone { get; private set; }
        public float AccumulatedDamage = 0;

        public AffectedDrone(Drone drone)
        {
            Drone = drone;
        }
    }
}
