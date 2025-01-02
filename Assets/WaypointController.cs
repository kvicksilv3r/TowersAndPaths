using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointController : MonoBehaviour
{
    public static WaypointController Instance;
    public List<WaypointPoint> waypoints = new List<WaypointPoint>();

    private void Awake()
    {
        SetupInstance();
    }

    public void RegisterPoint(WaypointPoint newPoint)
    {
        if (waypoints == null)
        {
            waypoints = new List<WaypointPoint>();
        }

        waypoints.Add(newPoint);
        UpdatePoints();
    }

    public void RemovePoint(WaypointPoint removedPoint)
    {
        waypoints.Remove(removedPoint);

        foreach (var waypoint in waypoints)
        {
            if (waypoint.nextPoints.Contains(removedPoint))
            {
                waypoint.nextPoints.Remove(removedPoint);
            }
        }

        UpdatePoints();
    }

    private void UpdatePoints()
    {
        if (waypoints.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            var point = waypoints[i];

            point.gameObject.name = $"Waypoint_{i}";

            point.nextPoints.RemoveAll(item => item == null);
        }
    }

    public WaypointPoint GetFirstWaypoint()
    {
        return waypoints[0];
    }

    public void SetupInstance()
    {
        Instance = this;
    }
}
