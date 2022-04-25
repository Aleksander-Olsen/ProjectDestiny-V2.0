using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;
using System;

[Serializable]
public class Shuttle : DataboxType
{
    [SerializeField]
    private ShuttleStatus status;

    public Shuttle() {
        status = ShuttleStatus.MISSING;
    }

    public Shuttle(ShuttleStatus _status) {
        status = _status;
    }

    public override void DrawEditor() {
        GUILayout.Label(status.ToString());
    }
}
