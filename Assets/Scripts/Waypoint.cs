using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Waypoint : MonoBehaviour
{
    public Vector3[] points = new Vector3[0];

    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    // Start is called before the first frame update
    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWaypointPosition(int index)
    {
        return CurrentPosition + points[index];
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(points[i] + _currentPosition, 0.5f);

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Waypoint))]

    public class WaypointEditor : Editor 
    {
        Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI(){}
    }

#endif
}
