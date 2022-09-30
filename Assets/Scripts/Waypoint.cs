using UnityEngine;

public class Waypoint: MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 firstPoint, Vector3 secondPoint)
    {
        lineRenderer.SetPosition(0, new Vector3(firstPoint.x, -0.2f, firstPoint.z));
        lineRenderer.SetPosition(1, new Vector3(secondPoint.x, -0.2f, secondPoint.z));
    }

}
