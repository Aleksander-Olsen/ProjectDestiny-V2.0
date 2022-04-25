using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public class Shield : ShipComponent
{
    private const float RECHARGE_RATE = 0.01f;

    private float MaxCharge { get => (1.0f - (brokenSections * brokeSectionSize)); }

    public Shield() {
        brokeSectionSize = 0.12f;

        brokenSections = PlayerController.random.Next(4, MAX_BROKEN_SECTIONS + 1);
        currentCharge = MaxCharge;
    }

    public override void AddCharge(float _power, float _deltaTime) {
        
        if (currentCharge < MaxCharge) {
            currentCharge += RECHARGE_RATE * _power * _deltaTime;
        }

        if (currentCharge > MaxCharge) {
            currentCharge = MaxCharge;
        }
    }

    public override void ResetCharge() {
        currentCharge = MaxCharge;
    }

    public void FixBrokenSection() {
        if (brokenSections > 0) {
            --brokenSections;
        }
    }

    public override void DrawEditor() {
        GUILayout.Space(10);
        GUILayout.Label("SHIELD");
        base.DrawEditor();
    }
}
