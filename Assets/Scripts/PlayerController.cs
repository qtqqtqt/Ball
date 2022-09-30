using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float waypointTolerance = 0.5f;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject waypointPrefab;

    Queue<Transform> waypoints = new();
    Transform previousWaypoint;

    private void Update()
    {
        ProcessRaycast();
        ProcessMovement();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnPoint(hit.point);
            }
        }
    }

    private void SpawnPoint(Vector3 spawnPosition)
    {
        if (previousWaypoint == null) previousWaypoint = transform;

        Transform newWaypoint = Instantiate(waypointPrefab, spawnPosition, Quaternion.identity).transform;
    
        newWaypoint.GetComponent<Waypoint>().DrawLine(newWaypoint.position, previousWaypoint.position);

        waypoints.Enqueue(newWaypoint.transform);
        previousWaypoint = newWaypoint;
    }

    private void ProcessMovement()
    {
        if (waypoints.Count == 0) return;

        MoveTo(GetCurrentWaypoint());

        if (AtWaypoint())
        {
            Destroy(waypoints.Dequeue().gameObject);
        }
    }

    private void MoveTo(Vector3 destination)
    {
        float newX = destination.x;
        float newZ = destination.z;

        Vector3 newDestination = new Vector3(newX, 0, newZ);
        transform.position = Vector3.MoveTowards(transform.position, newDestination, speed * Time.deltaTime);
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return waypoints.Peek().position;
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
