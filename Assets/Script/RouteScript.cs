using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteScript : MonoBehaviour
{
    public Transform[] points;
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100; // Nombre de points pour approximations de la courbe

    private void Start()
    {
        if (points.Length < 4)
        {
            Debug.LogError("Il faut au moins 4 points pour une courbe de Bézier cubique.");
            return;
        }
        
        if (lineRenderer == null)
        {
            Debug.LogError("Le LineRenderer n'est pas assigné.");
            return;
        }

        // Générer les points de la courbe et les affecter au LineRenderer
        Vector3[] curvePoints = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            curvePoints[i] = CalculateCubicBezierPoint(t, points[0].position, points[1].position, points[2].position, points[3].position);
        }

        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(curvePoints);
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
               3 * Mathf.Pow(1 - t, 2) * t * p1 +
               3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
               Mathf.Pow(t, 3) * p3;
    }
}
