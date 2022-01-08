using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager _weaponManager;

    // fire rate 
    public float fireRate =15f;
    private float _nextTimeToFire;
    // default damage
    public float damage = 20f;
    
    // zoom  & aim
    [SerializeField] private Animator _zoomCameraAnim; // showing in inspector to verify data flow : is present
    private bool _zoomed;
    private Camera _mainCam;
    private GameObject _crosshair;
    private bool _isAiming;

    [SerializeField]  private GameObject _findFPCamera; // showing in inspector to verify data flow : is present

    // spear & arrow
    [SerializeField] private GameObject _arrowPrefab, _spearPrefab;
    [SerializeField] private Transform _arrowSpearStartPosition;



    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>(); // WeaponManager script component on Player. Holds weapon choice/index

        // get zoom camera animator - find Look_Root, then find Zoom_Camera inside it, get the animator component
        //_zoomCameraAnim = transform.Find("Look Root").transform.Find("FP Camera").GetComponent<Animator>();
        //_zoomCameraAnim = GetComponentInChildren<Animator>();
        
        _findFPCamera = GameObject.Find(Tags.ZOOM_CAMERA);
        _zoomCameraAnim = _findFPCamera.GetComponent<Animator>();
        // avoid gameobject.find() - slow. if must use, use in awake/start  not update

        _crosshair = GameObject.FindGameObjectWithTag(Tags.CROSSHAIR);

        _mainCam = Camera.main;

    }


    // Start is called before the first frame update
    void Start()
    {
        //_zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
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
        if (_weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // HOLD down LEFT button, & shoot at regular interval if Time > next time to fire
            if (Input.GetMouseButton(0) && Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;

                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
            // below test mouse button fire
            //if (Input.GetMouseButton(0))
            //{
            //    _nextTimeToFire = Time.time + 1f / fireRate;

            //    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            //}
        }
        else // all other one shot weapons
        {
            if (Input.GetMouseButtonDown(0))
            {
                // axe handler
                if(_weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation(); 
                }

                // one shot weapon handler
                if(_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }

                // spear & arrow handler
                else 
                {
                    if (_isAiming) // _isAiming set in ZoominAndOut() after testing if selected weapon has Self_Aim tag
                    {
                        _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            // shoot arrow
                            FireArrowOrSpear(true);
                        }
                        else if (_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
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
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1)) // HOLD down RIGHT mouse button
            {
                // play zoom in animation
                _zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                // turn off _crosshair
                _crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1)) // RELEASE RIGHT mouse button
            {
                // play zoom out animation
                _zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                // turn on _crosshair
                _crosshair.SetActive(true);
            }
        }
        // test if selected weapon has Weapon Aim "SELF_AIM" tag
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(true); // set bool Aim to true to cue AIM animation state
                _isAiming = true;

            }
            if (Input.GetMouseButtonUp(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(false); 
                _isAiming = false;

            }
        }
    }

    private void BulletFired()
    {
        // raycast is an infinite line. Hit will old info of what it hits, and can use the contacted gameObjects.
        RaycastHit hit;
        if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out hit))
        // out causes data attached to the raycast hit to be passed to the RayCastHit and used. Passes info out.
        //{
        //    print("YOU HIT : " + hit.transform.gameObject.name); // prints to log name of target (if hit)
        //}
        {
            
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                print("YOU HIT : " + hit.transform.gameObject.name + " Damage : " + GetComponent<HealthScript>().damageDealt); ;
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }

    }

    void FireArrowOrSpear(bool fireArrow)
    {
        // true fire ARROW
        if (fireArrow)
        {
            GameObject arrow = Instantiate(_arrowPrefab);
            arrow.transform.position = _arrowSpearStartPosition.position;

            arrow.GetComponent<ArrowSpearScript>().Launch(_mainCam);
        }
        // false fire SPEAR
        else
        {
            GameObject spear = Instantiate(_spearPrefab);
            spear.transform.position = _arrowSpearStartPosition.position;

            spear.GetComponent<ArrowSpearScript>().Launch(_mainCam);
        }
    }
}
