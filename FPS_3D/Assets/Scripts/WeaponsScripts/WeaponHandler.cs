using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponAim { NONE, SELF_AIM, AIM }
public enum WeaponFireType { SINGLE, MULTIPLE }
public enum WeaponBulletType { BULLET, ARROW, SPEAR, NONE }


public class WeaponHandler : MonoBehaviour
{
    // Weapon ANIMATIONS & SOUNDS - Script component is ON EACH WEAPON

    [SerializeField] private Animator anim;

    public WeaponAim weaponAim;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER); // from animation controller / animator / state machines
    }
    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }
    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }





}
