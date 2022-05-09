using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveController : MonoBehaviour
{
    public const string RESOURCE_LOCATION = "Prefabs/Beziercurve";
    public const float LINE_STEP = 0.01f;

    [SerializeField]
    private BezierCurve curve;

    [SerializeField]
    private LineRenderer lineRenderer;

    private List<Vector3> lineRendererPoints;

    public BezierCurve Curve { get => curve; set => curve = value; }

    // Start is called before the first frame update
    void Awake()
    {
        curve = new BezierCurve();
        lineRendererPoints = new List<Vector3>();
    }

    public void Init(BezierCurveController _lastCurve, int _side) {

        if (_lastCurve == null) {
            curve.Init(null, _side);
        } else {
            transform.position = _lastCurve.transform.position + (_lastCurve.transform.forward * BezierCurve.CURVE_LENGTH);
            curve.Init(_lastCurve.Curve, _side);
        }

        CalculatePoints();
    }

    private void CalculatePoints() {
        float step = 0.0f;
        lineRendererPoints.Clear();

        while (step <= 1.0f) {
            lineRendererPoints.Add(curve.FindPointOnBezCurve(step));
            step += LINE_STEP;
        }

        lineRenderer.positionCount = lineRendererPoints.Count;
        lineRenderer.SetPositions(lineRendererPoints.ToArray());
    }
}
