using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    // fire rate 
    public float fireRate =15f;
    private float nextTimeToFire;
    // default damage
    public float damage = 20f;
    
    // zoom  & aim
    [SerializeField] private Animator zoomCameraAnim; // showing in inspector to verify data flow : is present
    private bool zoomed;
    private Camera mainCam;
    private GameObject crosshair;
    private bool isAiming;

    [SerializeField]  private GameObject findFPCamera; // showing in inspector to verify data flow : is present

    // spear & arrow
    [SerializeField] private GameObject arrowPrefab, spearPrefab;
    [SerializeField] private Transform arrowSpearStartPosition;



    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>(); // WeaponManager script component on Player. Holds weapon choice/index

        // get zoom camera animator - find Look_Root, then find Zoom_Camera inside it, get the animator component
        //zoomCameraAnim = transform.Find("Look Root").transform.Find("FP Camera").GetComponent<Animator>();
        //zoomCameraAnim = GetComponentInChildren<Animator>();
        
        findFPCamera = GameObject.Find(Tags.ZOOM_CAMERA);
        zoomCameraAnim = findFPCamera.GetComponent<Animator>();

        crosshair = GameObject.FindGameObjectWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    }


    // Start is called before the first frame update
    void Start()
    {
        //zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
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
                    if (isAiming) // isAiming set in ZoominAndOut() after testing if selected weapon has Self_Aim tag
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            // shoot arrow
                            FireArrowOrSpear(true);
                        }
                        else if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            // throw spear
                            FireArrowOrSpear(false);
                        }
                    }

                }
            }
        }
    }

    void ZoomInAndOut()
    {
        // test if selected weapon has Weapon Aim "AIM" tag
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1)) // HOLD down RIGHT mouse button
            {
                // play zoom in animation
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                // turn off crosshair
                crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1)) // RELEASE RIGHT mouse button
            {
                // play zoom out animation
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                // turn on crosshair
                crosshair.SetActive(true);
            }
        }
        // test if selected weapon has Weapon Aim "SELF_AIM" tag
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true); // set bool Aim to true to cue AIM animation state
                isAiming = true;

            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false); 
                isAiming = false;

            }
        }
    }

    private void BulletFired()
    {
        
    }

    void FireArrowOrSpear(bool fireArrow)
    {
        // true fire ARROW
        if (fireArrow)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = arrowSpearStartPosition.position;

            arrow.GetComponent<ArrowSpearScript>().Launch(mainCam);
        }
        // false fire SPEAR
        else
        {
            GameObject spear = Instantiate(spearPrefab);
            spear.transform.position = arrowSpearStartPosition.position;

            spear.GetComponent<ArrowSpearScript>().Launch(mainCam);
        }
    }
}
