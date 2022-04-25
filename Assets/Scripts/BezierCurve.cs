using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve {
    private Vector3[] ctrlPoints;

    BezierCurve() {
        ctrlPoints = new Vector3[4];
    }

    public Vector3 FindPointOnBezCurve(float t) {
        float oneMinusT = 1f - t;

        Vector3 p = Mathf.Pow(oneMinusT, 3) * ctrlPoints[0] +
             3f * Mathf.Pow(oneMinusT, 2) * t * ctrlPoints[1] +
             3f * oneMinusT * Mathf.Pow(t, 2) * ctrlPoints[2] +
             Mathf.Pow(t, 3) * ctrlPoints[3];

        return p;
    }
}
