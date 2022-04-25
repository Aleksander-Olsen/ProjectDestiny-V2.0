using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

[System.Serializable]
public abstract class ShipComponent : DataboxType {
    public static readonly int MAX_BROKEN_SECTIONS = 5;
    

    [SerializeField]
    protected float currentCharge;

    [SerializeField]
    protected int brokenSections;

    protected float brokeSectionSize = 0.1f;

    public float CurrentCharge { get => currentCharge; set => currentCharge = value; }
    public int BrokenSections { get => brokenSections; set => brokenSections = value; }
    public float BrokenSectionSize { get => brokeSectionSize; }

    public virtual int ActiveSections() { return MAX_BROKEN_SECTIONS - brokenSections; }
    public virtual void AddCharge(float _power, float _deltaTime) { }
    public virtual void ResetCharge() { currentCharge = 0.0f; }

    public override void DrawEditor() {
        GUILayout.Label("Broken sections: " + brokenSections.ToString());
    }
}
