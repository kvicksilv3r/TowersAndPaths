using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypointBehaviour
{
    FlipFlop,
    Random,
    Set
}

public class WaypointPoint : MonoBehaviour
{
    public List<WaypointPoint> nextPoints;
    public WaypointBehaviour defaultBehaviour;
    private WaypointBehaviour currentBehaviour;
    private int overrideDirection;
    private int flipflop = 0;

    public WaypointPoint specificNextPoint;

    private void Start()
    {
        currentBehaviour = defaultBehaviour;
    }

    public List<WaypointPoint> GetConnectedPoints()
    {
        if (nextPoints == null)
        {
            nextPoints = new List<WaypointPoint>();
        }

        return nextPoints;
    }

    public WaypointPoint GetNextWaypoint()
    {
        if (nextPoints.Count <= 0)
        {
            return null;
        }

        switch (currentBehaviour)
        {
            case WaypointBehaviour.FlipFlop:
                flipflop++;
                flipflop = flipflop % nextPoints.Count;
                return nextPoints[flipflop];

            case WaypointBehaviour.Random:
                return nextPoints[Random.Range(0, nextPoints.Count)];

            case WaypointBehaviour.Set:
                return nextPoints[overrideDirection];

            default: return null;
        }
    }

    public void SetOverridePoint(WaypointPoint point)
    {
        specificNextPoint = point;
    }

    private void OnDrawGizmos()
    {
        if (nextPoints.Count <= 0)
        {
            return;
        }

        foreach (var point in nextPoints)
        {
            if (!point)
            {
                continue;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, point.transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (nextPoints.Count <= 0)
        {
            return;
        }

        foreach (var point in nextPoints)
        {
            if (!point)
            {
                continue;
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, point.transform.position);
        }
    }
}
