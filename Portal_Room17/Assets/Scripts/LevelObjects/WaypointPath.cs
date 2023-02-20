using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // Helper method qui permet de récupérer l'index du waypoint vers lequel la plateforme va se diriger par la suite:
    public Transform GetWaypoint(int waypointIndex)
    {
        // Les waypoints sont des enfants de l'objet contenant ce script, on peut donc récupérer l'index avec GetChild:
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypoint(int currentWaypointIndex)
    {
        // Le prochain waypoint est forcément à l'index n+1, donc on l'initialise:
        int nextWaypointIndex = currentWaypointIndex + 1;

        return nextWaypointIndex;
    }
}
