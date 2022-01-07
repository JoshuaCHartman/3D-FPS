using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        _anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }
    public void Run(bool run)
    {
        _anim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }
    public void Attack() // trigger
    {
        _anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }
    public void Dead()
    {
        _anim.SetTrigger(AnimationTags.DEAD_TRIGGER);
    }

}
