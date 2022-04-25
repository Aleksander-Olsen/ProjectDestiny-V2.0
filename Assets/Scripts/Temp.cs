using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    [SerializeField]
    private Transform mainPower;

    private void Start() {
        mainPower.GetChild(mainPower.childCount - 1).GetChild(0).gameObject.SetActive(false);
    }
}
