using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

[System.Serializable]
public class Power : ShipComponent
{
    public const int MAX_SEXTIONS = 6;
    public const int MAX_CHARGE = 10;
    public const int MAX_SYSTEMS = 3;

    private int[] systems;

    public int ShieldCharge { get => systems[0]; set => systems[0] = value; }
    public int WeaponCharge { get => systems[1]; set => systems[1] = value; }
    public int HyperdriveCharge { get => systems[2]; set => systems[2] = value; }

    public override int ActiveSections() { return (MAX_SEXTIONS - brokenSections); }
    
    public Power() {
        brokeSectionSize = 5;
        brokenSections = MAX_BROKEN_SECTIONS;
        currentCharge = ActiveSections() * brokeSectionSize;

        systems = new int[MAX_SYSTEMS];
    }

    public void Init() {
        int charge = (ActiveSections() * (int)brokeSectionSize) / 3;
        WeaponCharge = charge;
        ShieldCharge = charge;
        HyperdriveCharge = charge;

        int mod = (ActiveSections() * (int)brokeSectionSize) % 3;
        switch (mod) {
            case 1:
                WeaponCharge++;
                break;
            case 2:
                ShieldCharge++;
                WeaponCharge++;
                break;
            default:
                break;
        }

        currentCharge = 0;
    }

    /*
     * Add or remove charge to a system.
     *      - _amount = charge amount (1 or -1)
     *      - _system = system to add or remove charge to
     */
    public bool AddChargeToSystem(int _amount, int _system) {
        
        //Abort if amount is invalid
        if (_amount < -1 || _amount == 0 || _amount > 1) {
            return false;
        }

        //Abort if invalid system
        if (_system < 0 || _system > (MAX_SYSTEMS - 1)) {
            return false;
        }

        //Abort if trying to add power to system when at full power
        if (_amount > 0 && systems[_system] >= MAX_CHARGE) {
            return false;
        }

        //Abort if trying to add charge when current charge is 0
        if (_amount > 0 && currentCharge <= 0) {
            return false;
        }

        //Abort if trying to remove charge from system when system charge is 0
        if (_amount < 0 && systems[_system] <= 0) {
            return false;
        }
        
        switch (_system) {
            case 0:
                ShieldCharge += _amount;
                break;
            case 1:
                WeaponCharge += _amount;
                break;
            case 2:
                HyperdriveCharge += _amount;
                break;
            default:
                break;
        }

        
        currentCharge += -_amount;
        //Debug.LogFormat("Current charge: {0}\nShield: {1}\nWeapons: {2}\nHyperdrive: {3}", currentCharge, ShieldCharge, WeaponCharge, HyperdriveCharge);
        return true;
    }
}
