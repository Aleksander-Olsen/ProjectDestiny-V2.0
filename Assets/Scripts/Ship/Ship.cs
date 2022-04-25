using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;
using System;
using System.Linq;

[Serializable]
public class Ship : DataboxType
{
    private static readonly int MAX_SHUTTLES = 2;
    private static readonly int MAX_SPECIALISTS = 6;

    [SerializeField]
    private string shipName;
    [SerializeField]
    private bool manualControl;
    [SerializeField]
    private int sensorLvl;

    [SerializeField]
    private Shuttle[] shuttles;
    [SerializeField]
    private Scientist[] scientists;
    [SerializeField]
    private Military[] military;

    [SerializeField]
    private Weapon leftWeapon;
    [SerializeField]
    private SpecialWeapon specialWeapon;
    [SerializeField]
    private Weapon rightWeapon;

    [SerializeField]
    private Shield shield;
    [SerializeField]
    private Hyperdrive hyperdrive;

    [SerializeField]
    private Power power;

    public bool ManualControl { get => manualControl; set => manualControl = value; }
    public Power Power { get => power; set => power = value; }
    public Hyperdrive Hyperdrive { get => hyperdrive; set => hyperdrive = value; }
    public Shield Shield { get => shield; set => shield = value; }
    public Weapon LeftWeapon { get => leftWeapon; set => leftWeapon = value; }
    public SpecialWeapon SpecialWeapon { get => specialWeapon; set => specialWeapon = value; }
    public Weapon RightWeapon { get => rightWeapon; set => rightWeapon = value; }

    public Ship() {
        shipName = "Destiny";
        manualControl = false;
        sensorLvl = 1;

        shuttles = new Shuttle[MAX_SHUTTLES];
        shuttles[0] = new Shuttle(ShuttleStatus.BROKEN);
        shuttles[1] = new Shuttle(ShuttleStatus.MISSING);

        scientists = new Scientist[MAX_SPECIALISTS];
        int specialists = PlayerController.random.Next(1, MAX_SPECIALISTS - 1);
        for (int i = 0; i < specialists; i++) {
            scientists[i] = new Scientist();
        }

        military = new Military[MAX_SPECIALISTS];
        specialists = PlayerController.random.Next(1, MAX_SPECIALISTS - 1);
        for (int i = 0; i < specialists; i++) {
            military[i] = new Military();
        }

        leftWeapon = new Weapon();
        specialWeapon = new SpecialWeapon();
        rightWeapon = new Weapon();

        shield = new Shield();
        hyperdrive = new Hyperdrive();

        power = new Power();
    }

    public void AddChargeToAllSystems(float _deltaTime) {
        shield.AddCharge(power.ShieldCharge, _deltaTime);

        rightWeapon.AddCharge(power.WeaponCharge, _deltaTime);
        specialWeapon.AddCharge(power.WeaponCharge, _deltaTime);
        leftWeapon.AddCharge(power.WeaponCharge, _deltaTime);

        hyperdrive.AddCharge(power.HyperdriveCharge, _deltaTime);
    }

    public void InitBattle() {
        shield.ResetCharge();

        rightWeapon.ResetCharge();
        specialWeapon.ResetCharge();
        leftWeapon.ResetCharge();

        hyperdrive.ResetCharge();
    }


    public override void DrawEditor() {
        GUILayout.Label("Ship name: " + shipName);
        string manCtrl = manualControl ? "YES" : "NO";
        GUILayout.Label("Manual control: " + manCtrl);
        GUILayout.Label("Sensor level: " + sensorLvl.ToString());

        GUILayout.Space(10);
        GUILayout.Label("SHUTTLES:");
        if (shuttles.All(x => x == null)) {
            GUILayout.Label("NO SHUTTLES!!");
        } else {
            foreach (Shuttle shuttle in shuttles) {
                if (shuttle != null) {
                    shuttle.DrawEditor();
                }
            }
        }

        GUILayout.Space(10);
        GUILayout.Label("SCIENTISTS:");
        if (scientists.All(x => x == null)) {
            GUILayout.Label("NO SCIENTISTS!!");
        }

        GUILayout.Space(10);
        GUILayout.Label("MILITARY PERSONNEL:");
        if (military.All(x => x == null)) {
            GUILayout.Label("NO MILITARY PERSONNEL!!");
        }

        GUILayout.Space(10);
        leftWeapon.DrawEditor();
        specialWeapon.DrawEditor();
        rightWeapon.DrawEditor();

        GUILayout.Space(10);
        shield.DrawEditor();
        hyperdrive.DrawEditor();

    }
}
