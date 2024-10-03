using UnityEngine;

public class DroneMovement
{
    private Transform _droneTransform;
    private DroneProgram _droneProgram;
    private DroneDataSO _droneData;

    private int _nextWaypointIndex = 0;

    public DroneMovement(Transform droneTransform, DroneProgram droneProgram, DroneDataSO droneData)
    {
        _droneTransform = droneTransform;
        _droneProgram = droneProgram;
        _droneData = droneData;
    }

    public void MovementTick()
    {
        Vector3 nextWaypointPosition = _droneProgram.Waypoints[_nextWaypointIndex].position;

        if (_droneTransform.position == nextWaypointPosition)
        {
            SelectNextWaypoint();
        }

        MoveTo(nextWaypointPosition);
    }

    private void MoveTo(Vector3 tragetPosition)
    {
        Vector3 lookPosition = tragetPosition - _droneTransform.position;

        if (lookPosition != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookPosition);

            if (_droneTransform.rotation != lookRotation)
            {
                _droneTransform.rotation = lookRotation;
            }
        }
        

        _droneTransform.position = Vector3.MoveTowards(_droneTransform.position, tragetPosition, _droneData.MovementSpeed * Time.deltaTime);
    }

    private void SelectNextWaypoint()
    {
        if (_nextWaypointIndex < _droneProgram.Waypoints.Length - 1)
        {
            _nextWaypointIndex++;
        }
        else
        {
            _nextWaypointIndex = 0;
        }

    }
}
