using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate =15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>(); // WeaponManager script component on Player. Holds weapon choice/index


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        // if have assault rifle with multiple shot ability
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // HOLD down LEFT button, & shoot at regular interval if Time > next time to fire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
            // below test mouse button fire
            //if (Input.GetMouseButton(0))
            //{
            //    nextTimeToFire = Time.time + 1f / fireRate;

            //    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            //}
        }
        else // all other one shot weapons
        {
            if (Input.GetMouseButtonDown(0))
            {
                // axe handler
                if(weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation(); 
                }

                // one shot weapon handler
                if(weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }

                // spear & arrow handler
                else 
                {

                }
                

            }
        }

    }

    private void BulletFired()
    {
        
    }
}
