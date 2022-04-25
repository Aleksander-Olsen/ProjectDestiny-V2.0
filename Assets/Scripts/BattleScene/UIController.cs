using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // Main power
    [Header("Main Power")]
    [SerializeField]
    private List<Transform> mainPowerBars;
    [SerializeField]
    private List<Transform> mainPowerBrokenSections;


    // Shield
    [Header("Shield")]
    [SerializeField]
    private List<Transform> shieldPowerBars;
    [SerializeField]
    private List<Transform> shieldBrokenSections;
    [SerializeField]
    private List<Button> shieldButtons;
    [SerializeField]
    private Slider shieldSlider;


    // Hyperdrive
    [Header("Hyperdrive")]
    [SerializeField]
    private List<Transform> hyperdrivePowerBars;
    [SerializeField]
    private List<Button> hyperdriveButtons;
    [SerializeField]
    private Slider hyperdriveSlider;


    // Weapons Main
    [Header("Weapons Power")]
    [SerializeField]
    private List<Transform> weaponPowerBars;
    [SerializeField]
    private List<Button> weaponButtons;


    // Left weapon
    [Header("Left Weapon")]
    [SerializeField]
    private List<Transform> leftWeaponBrokenSections;
    [SerializeField]
    private Button leftWeaponFireBtn;
    [SerializeField]
    private Slider leftWeaponSlider;


    // Special weapon
    [Header("Special Weapon")]
    [SerializeField]
    private List<Transform> specialWeaponBrokenSections;
    [SerializeField]
    private GameObject specialWeaponFullyBroken;
    [SerializeField]
    private GameObject specialWeaponInitialBuffer;
    [SerializeField]
    private Button specialWeaponFireBtn;
    [SerializeField]
    private Slider specialWeaponSlider;


    // Right weapon
    [Header("Right Weapon")]
    [SerializeField]
    private List<Transform> rightWeaponBrokenSections;
    [SerializeField]
    private Button rightWeaponFireBtn;
    [SerializeField]
    private Slider rightWeaponSlider;


    private Ship ship;

    // Start is called before the first frame update
    void Start() {
        ship = PlayerController.instance.Ship;
        StartCoroutine(WaitForDatabaseLoaded());
    }

    // Update is called once per frame
    void Update() {
        ship.AddChargeToAllSystems(Time.deltaTime);
        UpdateSliderUI();
    }
    

    public void Init() {
        ship.Power.Init();

        // Init main power broken sections
        for (int i = Power.MAX_SEXTIONS; i > ship.Power.ActiveSections(); i--) {
            mainPowerBrokenSections[i - 1].GetChild(0).gameObject.SetActive(true);
        }

        // Init main power bars
        for (int i = 0; i < (Power.MAX_SEXTIONS * ship.Power.BrokenSectionSize); i++) {
            if (i < (ship.Power.ActiveSections() * ship.Power.BrokenSectionSize)) {
                mainPowerBars[i].GetChild(0).gameObject.SetActive(true);
                mainPowerBars[i].GetChild(0).GetChild(0).gameObject.SetActive(false);
            } else {
                mainPowerBars[i].GetChild(0).gameObject.SetActive(false);
            }
        }


        // Init shield power bars
        for (int i = 0; i < ship.Power.ShieldCharge; i++) {
            shieldPowerBars[i].GetChild(0).gameObject.SetActive(true);
        }

        // Init shield broken sections
        for (int i = 0; i < Shield.MAX_BROKEN_SECTIONS; i++) {
            if ((i + 1) <= ship.Shield.ActiveSections()) {
                shieldBrokenSections[i].GetChild(0).gameObject.SetActive(false);
            } else {
                shieldBrokenSections[i].GetChild(0).gameObject.SetActive(true);
            }
        }

        // Disable shield add power button if at max power
        if (ship.Power.ShieldCharge >= Power.MAX_CHARGE) {
            shieldButtons[1].interactable = false;
        }


        // Init hyperdrive power bars
        for (int i = 0; i < ship.Power.HyperdriveCharge; i++) {
            hyperdrivePowerBars[i].GetChild(0).gameObject.SetActive(true);
        }

        // Disable hyperdrive add power button if at max power
        if (ship.Power.HyperdriveCharge >= Power.MAX_CHARGE) {
            hyperdriveButtons[1].interactable = false;
        }


        // Init weapon power bars
        for (int i = 0; i < ship.Power.WeaponCharge; i++) {
            weaponPowerBars[i].GetChild(0).gameObject.SetActive(true);
        }

        // Disable weapon add power button if at max power
        if (ship.Power.WeaponCharge >= Power.MAX_CHARGE) {
            weaponButtons[1].interactable = false;
        }

        // Init left weapon broken sections
        for (int i = 0; i < Weapon.MAX_BROKEN_SECTIONS; i++) {
            if ((i + 1) <= ship.LeftWeapon.ActiveSections()) {
                leftWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(false);
            } else {
                leftWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(true);
            }
        }

        // Init special weapon broken sections
        if (ship.SpecialWeapon.BrokenSections < Weapon.MAX_BROKEN_SECTIONS) {
            for (int i = 0; i < Weapon.MAX_BROKEN_SECTIONS; i++) {
                if ((i + 1) <= ship.SpecialWeapon.ActiveSections()) {
                    specialWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(false);
                } else {
                    specialWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(true);
                }
            }
        } else {
            foreach (Transform brokeSection in specialWeaponBrokenSections) {
                brokeSection.gameObject.SetActive(false);
            }
            specialWeaponInitialBuffer.SetActive(false);
            specialWeaponFullyBroken.SetActive(true);
        }

        // Init right weapon broken sections
        for (int i = 0; i < Weapon.MAX_BROKEN_SECTIONS; i++) {
            if ((i + 1) <= ship.RightWeapon.ActiveSections()) {
                rightWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(false);
            } else {
                rightWeaponBrokenSections[i].GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void IncreasePower(int _system) {
        if (ship.Power.AddChargeToSystem(1, _system)) {
            UpdatePowerUI(_system);
        }
    }

    public void DecreasePower(int _system) {
        if (ship.Power.AddChargeToSystem(-1, _system)) {
            UpdatePowerUI(_system);
        }
    }

    public void FireWeapon(int _weapon) {
        switch (_weapon) {
            case 0:
                ship.LeftWeapon.Fire();
                break;
            case 1:
                ship.SpecialWeapon.Fire();
                break;
            case 2:
                ship.RightWeapon.Fire();
                break;
            default:
                break;
        }

        UpdateSliderUI();
    }



    private void UpdatePowerUI(int _system) {
        switch (_system) {
            case 0:
                for (int i = 0; i < shieldPowerBars.Count; i++) {
                    if (i < ship.Power.ShieldCharge) {
                        shieldPowerBars[i].GetChild(0).gameObject.SetActive(true);
                    } else {
                        shieldPowerBars[i].GetChild(0).gameObject.SetActive(false);
                    }
                }

                UpdatePowerButtonUI(ship.Power.ShieldCharge, shieldButtons);
                break;
            case 1:
                for (int i = 0; i < weaponPowerBars.Count; i++) {
                    if (i < ship.Power.WeaponCharge) {
                        weaponPowerBars[i].GetChild(0).gameObject.SetActive(true);
                    } else {
                        weaponPowerBars[i].GetChild(0).gameObject.SetActive(false);
                    }
                }

                UpdatePowerButtonUI(ship.Power.WeaponCharge, weaponButtons);
                break;
            case 2:
                for (int i = 0; i < hyperdrivePowerBars.Count; i++) {
                    if (i < ship.Power.HyperdriveCharge) {
                        hyperdrivePowerBars[i].GetChild(0).gameObject.SetActive(true);
                    } else {
                        hyperdrivePowerBars[i].GetChild(0).gameObject.SetActive(false);
                    }
                }

                UpdatePowerButtonUI(ship.Power.HyperdriveCharge, hyperdriveButtons);
                break;
            default:
                break;
        }

        // Switch on/off main power UI
        for (int i = 0; i < (ship.Power.ActiveSections() * ship.Power.BrokenSectionSize); i++) {
            if (i < (int)ship.Power.CurrentCharge) {
                mainPowerBars[i].GetChild(0).GetChild(0).gameObject.SetActive(true);
            } else {
                mainPowerBars[i].GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    /* Activate or deactivate plus/minus buttons based on system power charge.
     *      _charge = system power charge
     *      _buttons = list containing the two buttons (plus and minus)
     */
    private void UpdatePowerButtonUI(int _charge, List<Button> _buttons) {
        // Saftey check to ensure there are two and only two buttons in list.
        if (_buttons.Count != 2) {
            Debug.LogError("Button list contains more or less than two buttons");
            return;
        }

        if (_charge > 0 && _charge < Power.MAX_CHARGE) {
            _buttons[0].interactable = true;
            _buttons[1].interactable = true;
        } else if (_charge == 0) {
            _buttons[0].interactable = false;
        } else if (_charge == Power.MAX_CHARGE) {
            _buttons[1].interactable = false;
        }
    }

    private void UpdateFireButtonUI(Weapon _weapon, Button _button) {
        if (_weapon.CurrentCharge >= _weapon.InitalBufferSize) {
            _button.interactable = true;
        } else {
            _button.interactable = false;
        }
    }

    private void UpdateSliderUI() {
        shieldSlider.value = ship.Shield.CurrentCharge;
        hyperdriveSlider.value = ship.Hyperdrive.CurrentCharge;

        leftWeaponSlider.value = ship.LeftWeapon.CurrentCharge;
        UpdateFireButtonUI(ship.LeftWeapon, leftWeaponFireBtn);
        specialWeaponSlider.value = ship.SpecialWeapon.CurrentCharge;
        UpdateFireButtonUI(ship.SpecialWeapon, specialWeaponFireBtn);
        rightWeaponSlider.value = ship.RightWeapon.CurrentCharge;
        UpdateFireButtonUI(ship.RightWeapon, rightWeaponFireBtn);
    }

    private IEnumerator WaitForDatabaseLoaded() {
        while (!DatabaseController.databaseLoaded) {
            yield return null;
        }

        ship.InitBattle();
        Init();
    }
}
