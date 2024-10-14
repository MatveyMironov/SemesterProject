using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private ProjectileParameters _parameters;
    private float _deathTime;
    private bool _hasParameters { get { return _parameters != null; } }

    private Vector3 _previousPosition;
    private float _lifeTime;

    public void SetParameters(ProjectileParameters parameters, float deathTime)
    {
        if (_hasParameters)
            return;

        _parameters = parameters;
        _deathTime = deathTime;
    }

    private void FixedUpdate()
    {
        if (!_hasParameters)
            return;

        Vector3 nextPosition = CalculateNextPosition();
        if (CheckForCollision(nextPosition, out RaycastHit hit))
        {
            transform.position = hit.point;
            OnCollision(hit);
            return;
        }

        _previousPosition = transform.position;
        transform.position = nextPosition;

        CountLifeTime();
    }

    private Vector3 CalculateNextPosition()
    {
        return transform.position + transform.forward * _parameters.Speed * Time.fixedDeltaTime;
    }

    private bool CheckForCollision(Vector3 nextPosition, out RaycastHit hit)
    {
        Vector3 collisionVector = nextPosition - _previousPosition;
        Vector3 collisionDirection = collisionVector.normalized;
        float collisionDistance = collisionVector.magnitude;

        if (Physics.Raycast(nextPosition, collisionDirection, out hit, collisionDistance, _parameters.HitableLayers))
        {
            return true;
        }

        return false;
    }

    protected abstract void OnCollision(RaycastHit hit);

    private void CountLifeTime()
    {
        _lifeTime += Time.fixedDeltaTime;

        if (_lifeTime >= _deathTime)
        {
            OnLifeTimeExpired();
        }
    }

    protected virtual void OnLifeTimeExpired()
    {
        Destroy(gameObject);
    }

    [Serializable]
    public class ProjectileParameters
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public LayerMask HitableLayers { get; private set; }
    }
}
