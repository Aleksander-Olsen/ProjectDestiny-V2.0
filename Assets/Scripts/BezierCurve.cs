using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BezierCurve {
    public const float CURVE_LENGTH = 100.0f;
    private const float MIN_CURVE_SWAY = 50.0f;
    private const float MAX_CURVE_SWAY = 160.0f;

    [SerializeField]
    private Vector3[] ctrlPoints;

    public Vector3[] CtrlPoints { get => ctrlPoints; }

    public BezierCurve() {
        ctrlPoints = new Vector3[4];
    }

    public void Init(BezierCurve _lastCurve, int _side) {
        if (_side == 0) {
            _side = PlayerController.GetRandomSide();
        }

        ctrlPoints[0] = Vector3.zero;
        ctrlPoints[2] = new Vector3(Random.Range(MIN_CURVE_SWAY, MAX_CURVE_SWAY) * _side, 0.0f, CURVE_LENGTH * (3.0f/4.0f));
        ctrlPoints[3] = new Vector3(0.0f, 0.0f, CURVE_LENGTH);

        // The second control point is dependent on the third control point of the last curve.
        if (_lastCurve == null) {
            ctrlPoints[1] = new Vector3(Random.Range(MIN_CURVE_SWAY, MAX_CURVE_SWAY) * _side, 0.0f, CURVE_LENGTH * (1.0f/4.0f));
        } else {
            Vector3 dir = _lastCurve.ctrlPoints[3] - _lastCurve.ctrlPoints[2];
            ctrlPoints[1] = dir;
        }

    }

    public Vector3 GetPointWorldPos(Vector3 _startPos, int _ctrlPoint) {
        if (_ctrlPoint < 1 || _ctrlPoint > 4) {
            Debug.LogError("Control point out of range!");
            return Vector3.zero;
        }

        return _startPos + ctrlPoints[_ctrlPoint - 1];
    }

    public void GetNewDirection(BezierCurve _lastCruve) {
        
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
