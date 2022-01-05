using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Select /switch different weapons

    [SerializeField] private WeaponHandler[] weapons; // hold weapons on player - currently 6. uses WeaponHandler script

    private int currentWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // make switch case

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
        if (currentWeaponIndex == weaponIndex)
            return;

        // turn off weapon/false, turn on/true selected weapon by index, assign index to current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false); // turn off/false current weapon by index number
        weapons[weaponIndex].gameObject.SetActive(true); // turn on/true weapon corresponding to passed int
        currentWeaponIndex = weaponIndex;   // set/store new current weapon index to the passed int
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[currentWeaponIndex];
    }

}
