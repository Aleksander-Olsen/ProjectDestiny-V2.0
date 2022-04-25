using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hyperdrive : ShipComponent
{
    private const float RECHARGE_RATE = 0.002f;

    public Hyperdrive() {
        currentCharge = 0.0f;
        brokenSections = MAX_BROKEN_SECTIONS;
    }

    public override void AddCharge(float _power, float _deltaTime) {
        if (currentCharge < 1.0f) {
            float newRechargeRate = RECHARGE_RATE * (MAX_BROKEN_SECTIONS - brokenSections + 1);
            currentCharge += newRechargeRate * _power * _deltaTime;
        }

        if (currentCharge > 1.0f) {
            currentCharge = 1.0f;
        }
    }

    public override void DrawEditor() {
        GUILayout.Space(10);
        GUILayout.Label("HYPERDRIVE");
        base.DrawEditor();
    }
}
