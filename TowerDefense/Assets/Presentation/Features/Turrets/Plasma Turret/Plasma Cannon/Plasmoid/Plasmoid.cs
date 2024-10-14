using System;
using UnityEngine;

public class Plasmoid : Projectile
{
    [SerializeField] private PlasmaCloud plasmaCloudPrefab;

    private PlasmoidParameters _parameters;
    private bool _hasParameters { get { return _parameters != null; } }

    public void SetParameters(PlasmoidParameters parameters, float explosionTime)
    {
        if (_hasParameters)
            return;

        SetParameters(parameters.ProjectileParameters, explosionTime);

        _parameters = parameters;
    }

    protected override void OnCollision(RaycastHit hit)
    {
        Explode();
    }

    protected override void OnLifeTimeExpired()
    {
        Explode();
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

    [Serializable]
    public class PlasmoidParameters
    {
        [field: SerializeField] public ProjectileParameters ProjectileParameters { get; private set; }

        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public int ExplosionDamage { get; private set; }
        [field: SerializeField] public float PlasmaCloudRadius { get; private set; }
        [field: SerializeField] public int PlasmaCloudDPS { get; private set; }
        [field: SerializeField] public float PlasmaCloudDecayTime { get; private set; }
        [field: SerializeField] public LayerMask AffectedLayers { get; private set; }
    }
}
