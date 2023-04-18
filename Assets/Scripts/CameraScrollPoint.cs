using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraScrollPoint : MonoBehaviour
{
    public string label = "no-label";
    public Color color = Color.red;
    public float radius = 0.5f;

    void OnDrawGizmos() 
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
#if UNITY_EDITOR
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(transform.position + transform.up * radius * 2, label, style);
#endif
    }
}
