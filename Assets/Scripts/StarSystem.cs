using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;
using System;

[Serializable]
public class StarSystem : DataboxType
{
    [SerializeField]
    private string systemName;
    [SerializeField]
    private List<Star> stars;
    [SerializeField]
    private List<Planet> planets;

    public string SystemName { get => systemName; set => systemName = value; }
    public List<Star> Stars { get => stars; set => stars = value; }
    public List<Planet> Planets { get => planets; set => planets = value; }

    public StarSystem() {
        systemName = "SOL";
        stars = new List<Star>();
        planets = new List<Planet>();
    }
}
