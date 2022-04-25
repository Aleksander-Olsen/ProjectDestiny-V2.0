using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Moon : CelestialBody
{
    [SerializeField]
    private MoonSize size;
    [SerializeField]
    private Environment env;

    public MoonSize Size { get => size; set => size = value; }
    public Environment Env { get => env; set => env = value; }

    public Moon() {
        bodyName = "";

        int rand = Random.Range(0, Enum.GetValues(typeof(MoonSize)).Length);
        size = (MoonSize)rand;

        rand = Random.Range(0, Enum.GetValues(typeof(Environment)).Length);
        env = (Environment)rand;
    }

    public Moon(string _name, MoonSize _size, Environment _env) {
        bodyName = _name;
        size = _size;
        env = _env;
    }
}
