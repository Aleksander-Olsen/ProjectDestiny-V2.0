using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;

    [SerializeField]
    private Button resumeBtn;
    [SerializeField]
    private Button newGameBtn;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init() {
        if (PlayerController.instance.Ship.ManualControl) {
            resumeBtn.interactable = true;
        }
    }
}
