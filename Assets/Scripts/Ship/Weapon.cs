using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public class Weapon : ShipComponent {

    private const float MAX_CHARGE = 1.0f;
    private const float CHARGE_CONSTANT = 0.01f;
    private const float DECREASE_RATE = 0.03f;

    protected float initalBufferSize;
    protected float initialChargeRate;
    protected float mainChargeRate;
    protected float overloadChargeRate;

    [SerializeField]
    protected WeaponType type;

    public float InitalBufferSize { get => initalBufferSize; set => initalBufferSize = value; }

    public Weapon() {
        brokeSectionSize = 0.1f;

        initalBufferSize = 0.15f;
        initialChargeRate = 6.0f;
        mainChargeRate = 1.5f;
        overloadChargeRate = 1.0f;

        currentCharge = 0.0f;
        brokenSections = PlayerController.random.Next(4, MAX_BROKEN_SECTIONS + 1);
    }

    public void Fire() {
        ResetCharge();
    }

    public override void AddCharge(float _power, float _deltaTime) {
        if (_power <= 0) {
            if (currentCharge > 0.0f) {
                currentCharge -= DECREASE_RATE * _deltaTime;
            }
            return;
        }

        if (currentCharge < initalBufferSize) {
            currentCharge += CHARGE_CONSTANT * (Mathf.Pow(1.21f, _power) + initialChargeRate) * _deltaTime;
        } else if (currentCharge < (MAX_CHARGE - (brokenSections * brokeSectionSize))) {
            currentCharge += CHARGE_CONSTANT * (Mathf.Pow(1.2f, _power) + mainChargeRate) * _deltaTime;
        } else if (currentCharge < (MAX_CHARGE + brokeSectionSize)) {
            currentCharge += CHARGE_CONSTANT * overloadChargeRate * _deltaTime;
        } else {
            currentCharge = MAX_CHARGE + brokeSectionSize;
        }
    }

    public override void DrawEditor() {
        GUILayout.Label("WEAPON");
        GUILayout.Label("TYPE: " + type.ToString());
        base.DrawEditor();
    }
}
