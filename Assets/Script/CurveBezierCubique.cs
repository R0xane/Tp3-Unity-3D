using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCubic : MonoBehaviour
{

     public Transform[] points; 
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100; 
    private Vector3 positions;

    private void Update()
    {
        

        
        Vector3[] curvePoints = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            curvePoints[i] = CalculateCubicBezierPoint(t, points[0].position, points[1].position, points[2].position, points[3].position);
        }

    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
               3 * Mathf.Pow(1 - t, 2) * t * p1 +
               3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
               Mathf.Pow(t, 3) * p3;
    }

    private void OnDrawGizmos()
    {
       
        for (float t = 0; t <= 1; t += 0.05f)
        {
            positions = CalculateCubicBezierPoint(t, points[0].position, points[1].position, points[2].position, points[3].position);
            Gizmos.DrawSphere(positions, 0.25f); 
        }

        
        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[1].position, points[2].position);
        Gizmos.DrawLine(points[2].position, points[3].position);
    }



}