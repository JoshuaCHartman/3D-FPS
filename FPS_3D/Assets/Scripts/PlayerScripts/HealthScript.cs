using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private NavMeshAgent _navAgent;
    private EnemyController _enemyController;

    public float damageDealt; // add to display damage amountin console / testing

    public bool isPlayer, isBoar, isCannibal;
    private bool _isDead;

    private EnemyAudio _enemyAudio;

    // health and UI
    public float health = 100f;
    private PlayerStatistics _playerStatistics;


    private void Awake()
    {
        // references
        if (isBoar || isCannibal)
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _enemyController = GetComponent<EnemyController>();
            _navAgent = GetComponent<NavMeshAgent>();

            // get AUDIO enemy
            _enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if (isPlayer)
        {
            _playerStatistics = GetComponent<PlayerStatistics>();
        }
    }
     public float ApplyDamage(float damage)
    {
        // check dead
        if (_isDead)
        {
            print("Death! Damage done was : " + damage);
            
        }

        // decrease health total by damage passed into method
        health -= damage;

        if (isPlayer)
        {
            // health total in UI
            _playerStatistics.DisplayHealthStats(health); // pass in new health total after damage taken. will adjust 
                                                            // fill % on health bar as new health / 100
            
        }

        if (isBoar || isCannibal)
        {
            print("Damage done was : " + damage);
            
            // when damaging enemy from afar, chase distance increased so enemy will move to chase state
            // PlayerAttack script BulletFired();
            if (_enemyController.EnemyState == EnemyState.PATROL)
            {
                _enemyController.chaseDistance = 50f;
                print("Damage done was : " + damage);
                
            }
            

        }

        if (health <= 0f)
        {
            print("Death! Damage done was : " + damageDealt);
            PlayerDied();
            _isDead = true;
            
            
        }
        return damage;
    }

    void PlayerDied()
    {
        if (isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            //GetComponent<BoxCollider>().isTrigger = false;
            // no death animation
            // not working add torque
            GetComponent<Rigidbody>().AddRelativeTorque(-transform.forward * 50f);

            //turn off enemy
            _enemyController.enabled = false;
            _navAgent.enabled = false;
            _enemyAnimator.enabled = false;

            // start coroutine - sounds
            StartCoroutine(DeathSound());

            EnemyManager.instance.EnemyDied(true); // true = cannibal died

        }

        if (isBoar)
        {
            _navAgent.velocity = Vector3.zero;
            _navAgent.isStopped = true;
            _enemyAnimator.enabled = false;

            _enemyAnimator.Dead();

            // start coroutine - sounds
            StartCoroutine(DeathSound());

            // enemy manager = spawn an enemy
            EnemyManager.instance.EnemyDied(false); // false = boar died
        }

        if (isPlayer)
        {
            // turn enemies off
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i =0; i < enemies.Length; i++)
            {
                // end the enemy controller or the enemy will continue after respawned new PLAYER
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // stop spawning in enemy manager
            EnemyManager.instance.StopSpawningEnemies();

            // turn off player
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); // restarts scene - no exit /start / load screen currently
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    // Coroutine - wait for seconds
    IEnumerator DeathSound()
    {
        yield return new WaitForSeconds(0.3f);
        _enemyAudio.PlayDeathSound();
    }
}
