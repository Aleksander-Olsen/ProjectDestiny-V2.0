using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Planet : CelestialBody
{
    [SerializeField]
    private PlanetSize size;
    [SerializeField]
    private Environment env;
    [SerializeField]
    private List<Moon> moons;

    public PlanetSize Size { get => size; set => size = value; }
    public Environment Env { get => env; set => env = value; }
    public List<Moon> Moons { get => moons; set => moons = value; }

    public Planet() {
        bodyName = "";

        int rand = Random.Range(0, Enum.GetValues(typeof(PlanetSize)).Length);
        size = (PlanetSize)rand;

        rand = Random.Range(0, Enum.GetValues(typeof(Environment)).Length);
        env = (Environment)rand;

        moons = new List<Moon>();
    }

    public Planet(string _name, PlanetSize _size, Environment _env) {
        bodyName = _name;
        size = _size;
        env = _env;
        moons = new List<Moon>();
    }
}
