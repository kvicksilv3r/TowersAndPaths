using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.Progress;
using System.Linq;

[CustomEditor(typeof(WaypointPoint))]
[CanEditMultipleObjects]
public class WaypointEditor : Editor
{
    SerializedProperty waypointBehaviour;
    SerializedProperty specificWaypoint;
    int index;

    private void OnEnable()
    {
        waypointBehaviour = serializedObject.FindProperty("defaultBehaviour");
        specificWaypoint = serializedObject.FindProperty("specificNextPoint");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        waypointBehaviour = serializedObject.FindProperty("defaultBehaviour");
        var wp = target as WaypointPoint;
        WaypointPoint removePoint = null;




        //Behaviour area

        EditorGUILayout.PropertyField(waypointBehaviour);
        GUILayout.Space(10);



        #region Connections

        GUILayout.Label("Connected points");

        foreach (var point in wp.nextPoints)
        {
            if (!point)
            {
                removePoint = point;
                break;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(point.name);

            if (wp.defaultBehaviour == WaypointBehaviour.Set && wp.specificNextPoint != point)
            {
                if (GUILayout.Button("Set next point", GUILayout.MaxWidth(125)))
                {
                    wp.SetOverridePoint(point);
                }
            }

            if (GUILayout.Button("Delete connection", GUILayout.MaxWidth(375)))
            {
                removePoint = point;
            }

            GUILayout.EndHorizontal();
        }

        if (removePoint)
        {
            wp.nextPoints.Remove(removePoint);
            removePoint = null;
        }

        #endregion

        #region New point

        if (GUILayout.Button("NewPoint"))
        {
            var point = Resources.Load<GameObject>("Waypoint") as GameObject;
            var newPoint = PrefabUtility.InstantiatePrefab(point) as GameObject;
            newPoint.transform.parent = wp.transform.parent;
            newPoint.transform.position = wp.transform.position;

            var wpc = FindObjectOfType<WaypointController>() as WaypointController;
            wpc.RegisterPoint(newPoint.GetComponent<WaypointPoint>());

            wp.nextPoints.Add(newPoint.GetComponent<WaypointPoint>());

            EditorUtility.SetDirty(wp);

            Selection.activeGameObject = newPoint;
        }

        serializedObject.ApplyModifiedProperties();

        #endregion

        #region Delete object area

        if (GUILayout.Button("DELETE"))
        {
            var wpc = FindObjectOfType<WaypointController>() as WaypointController;
            wpc.RemovePoint(wp);
            DestroyImmediate(wp.gameObject);
        }

        #endregion
    }
}
