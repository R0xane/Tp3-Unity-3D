using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierQuadratique : MonoBehaviour
{
    public Transform[] points;
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100;// Nombre de points pour approximations de la courbe
    private Vector3 positions;

    private void Update()
    {
        if (points.Length < 3)
        {
            Debug.LogError("Il faut au moins 3 points pour une courbe de Bézier quadratique.");
            return;
        }
        
        if (lineRenderer == null)
        {
            Debug.LogError("Le LineRenderer n'est pas assigné.");
            return;
        }

        
        Vector3[] curvePoints = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            curvePoints[i] = CalculateQuadraticBezierPoint(t, points[0].position, points[1].position, points[2].position);
        }

        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(curvePoints);
    }

    
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }

    private void OnDrawGizmos()
    {
        if (points.Length < 3)
        {
            return;
        }

        for (float t = 0; t <= 1; t += 0.05f)
        {
            positions = CalculateQuadraticBezierPoint(t, points[0].position, points[1].position, points[2].position);
            Gizmos.DrawSphere(positions, 0.1f); 
        }

        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[1].position, points[2].position);
    }
}
