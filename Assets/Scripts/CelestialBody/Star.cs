using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Star : CelestialBody
{
    [SerializeField]
    private StarType type;
    [SerializeField]
    private StarSize size;

    public StarType Type { get => type; set => type = value; }
    public StarSize Size { get => size; set => size = value; }

    public Star() {
        bodyName = "";

        int rand = Random.Range(0, Enum.GetNames(typeof(StarType)).Length);
        type = (StarType)rand;

        rand = Random.Range(0, Enum.GetNames(typeof(StarSize)).Length);
        size = (StarSize)rand;
    }

    public Star(string _name, StarType _type, StarSize _size) {
        bodyName = _name;
        type = _type;
        size = _size;
    }
}
