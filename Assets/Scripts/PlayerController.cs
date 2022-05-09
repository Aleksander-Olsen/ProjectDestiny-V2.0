using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public static readonly Random random = new Random();

    [SerializeField]
    private Ship ship;

    public Ship Ship { get => ship; set => ship = value; }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateManualControl() {
        if (ship != null) {
            ship.ManualControl = true;
            DatabaseController.instance.RuntimeData.SaveDatabase();
        }
    }

    public static int GetRandomSide() {
        return random.Next(2) * 2 - 1;
    }
}
