using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;
using System;

[Serializable]
public abstract class CelestialBody : DataboxType
{
    [SerializeField]
    protected string bodyName;

    public string BodyName { get => bodyName; set => bodyName = value; }
}
