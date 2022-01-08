using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private NavMeshAgent _navAgent;
    private EnemyController _enemyController;

    public float health = 100f;

    public bool isPlayer, isBoar, isCannibal;
    private bool _isDead;

    private void Awake()
    {
        // references
        if (isBoar || isCannibal)
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _enemyController = GetComponent<EnemyController>();
            _navAgent = GetComponent<NavMeshAgent>();

            // get AUDIO enemy
        }
        if (isPlayer)
        {
            // player stats
        }
    }
     public void ApplyDamage(float damage)
    {
        // check dead
        if (_isDead)
        {
            return;
        }

        // decrease health total by damage passed into method
        health -= damage;

        if (isPlayer)
        {
            // health total in UI
        }

        if (isBoar || isCannibal)
        {
            // when damaging enemy from afar, chase distance increased so enemy will move to chase state
            // PlayerAttack script BulletFired();
            if (_enemyController.EnemyState == EnemyState.PATROL)
            {
                _enemyController.chaseDistance = 50f;
            }
        }

    }



}
