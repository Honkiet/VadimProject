using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaivior : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PatrolPath patrolPath;
    [SerializeField] Mover mover;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;


    Vector3 guardPosition;

    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;

    private void Start()
    {

        guardPosition = transform.position;
    }

    private void Update()
    {
        PatrolBehaviour();


        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

}
