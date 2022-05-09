using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelController : MonoBehaviour
{
    public static TravelController instance;

    public const int MAX_CURVES = 4;

    [SerializeField]
    private GameObject mainPathContainer;
    [SerializeField]
    private GameObject ship;

    private List<BezierCurveController> mainPath;

    private int curveSide;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        mainPath = new List<BezierCurveController>();
        curveSide = PlayerController.GetRandomSide();
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerController.instance.ActivateManualControl();

        for (int i = 0; i < MAX_CURVES; i++) {
            InitCurve(i);
        }

        ship.transform.position = mainPath[1].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitCurve(int _curveNum) {
        GameObject curve = Instantiate(Resources.Load<GameObject>(BezierCurveController.RESOURCE_LOCATION));
        curve.name = "Curve " + (_curveNum + 1).ToString();
        BezierCurveController curveController = curve.GetComponent<BezierCurveController>();
        mainPath.Add(curveController);

        curveSide *= -1;

        if (_curveNum == 0) {
            curveController.Init(null, curveSide);
        } else {
            curveController.Init(mainPath[_curveNum - 1], curveSide);
        }
    }
}
