using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5f;
    public int currentWaypointIndex = 0;

    void Update()
    {
        // Check if waypoints array is initialized and has elements
        if (waypoints != null && waypoints.Length > 0)
        {
            // Move the cube towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // Check if the cube has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                // Update the current waypoint index
                currentWaypointIndex++;

                // Check if we reached the end of the waypoints array
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;  // Reset to the first waypoint
                }
            }
        }
    }
}
