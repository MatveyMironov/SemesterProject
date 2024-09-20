using UnityEngine;

public class Target : Unit
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private bool isMoving = true;

    private Transform _selectedWaypoint;
    private int _selectedWaypointIndex = -1;

    private void Start()
    {
        SelectNextWaypoint();
    }

    private void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        if (!isMoving)
            return;

        if (_selectedWaypoint != null && transform.position != _selectedWaypoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _selectedWaypoint.position, speed * Time.deltaTime);
        }
        else
        {
            SelectNextWaypoint();
        }
    }

    private void SelectNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            isMoving = false;
            return;
        }

        if (_selectedWaypointIndex == waypoints.Length - 1)
        {
            _selectedWaypointIndex = 0;
        }
        else
        {
            _selectedWaypointIndex++;
        }
        _selectedWaypoint = waypoints[_selectedWaypointIndex];
    }
}
