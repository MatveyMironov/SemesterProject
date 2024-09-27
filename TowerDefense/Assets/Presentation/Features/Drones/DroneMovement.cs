using System;
using UnityEngine;

[Serializable]
public class DroneMovement
{
    [SerializeField] private Transform drone;

    [SerializeField] private float movementSpeed;

    [Space]
    [SerializeField] private Transform[] waypoints = new Transform[0];
    [SerializeField] private bool isMoving;

    private int nextWaypointIndex = 0;

    public void Tick()
    {
        if (!isMoving)
            return;

        Vector3 nextWaypointPosition = waypoints[nextWaypointIndex].position;

        if (drone.position == nextWaypointPosition)
        {
            SelectNextWaypoint();
        }

        MoveTo(nextWaypointPosition);
    }

    private void MoveTo(Vector3 tragetPosition)
    {
        Vector3 lookPosition = tragetPosition - drone.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookPosition);

        if (drone.rotation != lookRotation)
        {
            drone.rotation = lookRotation;
        }

        drone.position = Vector3.MoveTowards(drone.position, tragetPosition, movementSpeed * Time.deltaTime);
    }

    private void SelectNextWaypoint()
    {
        if (nextWaypointIndex < waypoints.Length - 1)
        {
            nextWaypointIndex++;
        }
        else
        {
            nextWaypointIndex = 0;
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isMoving)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoints[nextWaypointIndex].position, 0.1f);
        }
    }
#endif
}
