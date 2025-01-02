using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointInitiatior : EditorWindow
{
	[MenuItem("TFD/NewWaypoint")]
	public static void Initiate()
	{
		var wpc = FindObjectOfType<WaypointController>() as WaypointController;
		if (!wpc)
		{
			var gm = GameObject.Find("GameManager");

			if (!gm)
			{
				gm = new GameObject();
				gm.name = "GameManager";
			}
			wpc = gm.AddComponent<WaypointController>() as WaypointController;
		}

		var existingPoint = FindObjectOfType<WaypointPoint>();

		if (!existingPoint)
		{
			var point = Resources.Load<GameObject>("Waypoint") as GameObject;
			var newPoint = PrefabUtility.InstantiatePrefab(point) as GameObject;

			if (!WaypointController.Instance)
			{
				wpc.SetupInstance();
			}

			WaypointController.Instance.RegisterPoint(newPoint.GetComponent<WaypointPoint>());
			Selection.activeGameObject = newPoint;
		}

		else
		{
			Selection.activeGameObject = existingPoint.gameObject;
		}
	}
}
