using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Select /switch different weapons

    [SerializeField] private WeaponHandler[] _weapons; // hold weapons on player - currently 6. uses WeaponHandler script w/ _anim & sounds

    private int _currentWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        _currentWeaponIndex = 0; // will make default weapon 0 slot - currently axe 
        _weapons[_currentWeaponIndex].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // make switch case?

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }

    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        // handling to prevent redraw same weapon
        if (_currentWeaponIndex == weaponIndex)
            return;

        // turn off weapon/false, turn on/true selected weapon by index, assign index to current weapon
        _weapons[_currentWeaponIndex].gameObject.SetActive(false); // turn off/false current weapon by index number
        _weapons[weaponIndex].gameObject.SetActive(true); // turn on/true weapon corresponding to passed int
        _currentWeaponIndex = weaponIndex;   // set/store new current weapon index to the passed int
    }

    public WeaponHandler GetCurrentSelectedWeapon() // used in PlayerAttack script
    {
        return _weapons[_currentWeaponIndex];
    }

}
