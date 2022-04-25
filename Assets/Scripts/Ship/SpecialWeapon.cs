using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialWeapon : Weapon {
    

    public SpecialWeapon() : base() {
        brokeSectionSize = 0.16f;
        initalBufferSize = 0.2f;

        initialChargeRate = 5.0f;
        mainChargeRate = 1.0f;

        type = WeaponType.CHARGE_SHOT;
        brokenSections = MAX_BROKEN_SECTIONS;
    }

    public SpecialWeapon(WeaponType _type) : base() {
        brokeSectionSize = 0.16f;
        initalBufferSize = 0.2f;

        initialChargeRate = 5.0f;
        mainChargeRate = 1.0f;

        type = _type;
        brokenSections = MAX_BROKEN_SECTIONS;
    }

    public override void AddCharge(float _power, float _deltaTime) {
        if (brokenSections < MAX_BROKEN_SECTIONS) {
            base.AddCharge(_power, _deltaTime);
        }
    }

    public override void DrawEditor() {
        GUILayout.Label("SPECIAL");
        base.DrawEditor();
    }
}
