using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelController : MonoBehaviour
{
    public static TravelController instance;

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
        PlayerController.instance.ActivateManualControl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
