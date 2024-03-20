using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Waypoint : MonoBehaviour
{
    [SerializeField] Color color = Color.blue;
    // Vertical line dimensions
    readonly float lineHeight = 2f;

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = color;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + (Vector3.up * lineHeight);
        Handles.DrawWireDisc(startPoint, Vector3.up, 0.25f);
        Handles.DrawLine(startPoint, endPoint);
        Handles.DrawWireDisc(endPoint, Vector3.up, 0.25f);
    }
#endif
}