using System;
using UnityEngine;

public class Plasmoid : MonoBehaviour
{
    [SerializeField] private PlasmaCloud plasmaCloudPrefab;

    private PlasmoidParameters _parameters;
    private bool _hasParameters;

    private float _explosionTime;
    private float _lifeTime;

    private void FixedUpdate()
    {
        if (!_hasParameters)
            return;

        Move();
        CountLifeTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasParameters)
            return;

        Explode();
    }

    private void Move()
    {
        transform.position += transform.forward * _parameters.Speed * Time.fixedDeltaTime;
    }

    private void CountLifeTime()
    {
        _lifeTime += Time.fixedDeltaTime;

        if (_lifeTime >= _explosionTime)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, _parameters.ExplosionRadius, _parameters.AffectedLayers);
        foreach (Collider affectedObject in affectedObjects)
        {
            if (affectedObject.TryGetComponent(out DroneHealth droneHealth))
            {
                droneHealth?.SubtractHealth(_parameters.ExplosionDamage);
            }
        }

        PlasmaCloud plasmaCloud = Instantiate(plasmaCloudPrefab, transform.position, Quaternion.identity);
        plasmaCloud.SetParametres(_parameters.PlasmaCloudRadius, _parameters.PlasmaCloudDPS, _parameters.PlasmaCloudDecayTime);
        Destroy(gameObject);
    }

    public void SetParameters(PlasmoidParameters parameters, float explosionTime)
    {
        if (_hasParameters)
            return;

        _parameters = parameters;
        _explosionTime = explosionTime;
        _hasParameters = true;
    }

    [Serializable]
    public class PlasmoidParameters
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public int ExplosionDamage { get; private set; }
        [field: SerializeField] public float PlasmaCloudRadius { get; private set; }
        [field: SerializeField] public int PlasmaCloudDPS { get; private set; }
        [field: SerializeField] public float PlasmaCloudDecayTime { get; private set; }
        [field: SerializeField] public LayerMask AffectedLayers { get; private set; }

        public PlasmoidParameters(
            float speed,
            float explosionRadius,
            int explosionDamage,
            float plasmaCloudRadius,
            int plasmaCloudDPS,
            float plasmaCloudDecayTime,
            LayerMask affectedLayers)
        {
            Speed = speed;
            ExplosionRadius = explosionRadius;
            ExplosionDamage = explosionDamage;
            PlasmaCloudRadius = plasmaCloudRadius;
            PlasmaCloudDPS = plasmaCloudDPS;
            PlasmaCloudDecayTime = plasmaCloudDecayTime;
            AffectedLayers = affectedLayers;
        }
    }
}
