using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponAim { NONE, SELF_AIM, AIM }
public enum WeaponFireType { SINGLE, MULTIPLE }
public enum WeaponBulletType { BULLET, ARROW, SPEAR, NONE }


public class WeaponHandler : MonoBehaviour
{
    // Weapon ANIMATIONS & SOUNDS - Script component is ON EACH WEAPON

    [SerializeField] private Animator _anim;

    public WeaponAim weaponAim;

    [SerializeField] private GameObject _muzzleFlash;

    [SerializeField] private AudioSource _shootSound, _reloadSound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        _anim.SetTrigger(AnimationTags.SHOOT_TRIGGER); // from animation controller / animator / state machines
    }
    public void Aim(bool canAim)
    {
        _anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }
    public void TurnOnMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
    }
    public void TurnOffMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }
    public void PlayShootSound()
    {
        _shootSound.Play();
    }
    public void PlayReloadSound()
    {
        _reloadSound.Play();
    }
    public void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    public void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }





}
