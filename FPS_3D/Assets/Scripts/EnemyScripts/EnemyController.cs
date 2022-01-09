using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyState { PATROL, CHASE, ATTACK }

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator _enemyAnim;
    private NavMeshAgent _navAgent;
    private EnemyState _enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 7f;
    private float _currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;

    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    private float _patrolTimer;

    public float waitBeforeAttack;
    private float _attackTimer;

    private Transform _targetTF; // for enemy navmeshagent to target player's location

    public GameObject attackPoint; // serves for melee combat. contains attack script

    private EnemyAudio _enemyAudio; 

    private void Awake()
    {
        // get references
        _enemyAnim = GetComponent<EnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();

        _targetTF = GameObject.FindWithTag(Tags.PLAYER_TAG).transform; // get players coordinates

        _enemyAudio = GetComponentInChildren<EnemyAudio>();

    }


    // Start is called before the first frame update
    void Start()
    {
        // patrolling - initial state
        _enemyState = EnemyState.PATROL; // inital state
        _patrolTimer = patrolForThisTime;
        // time difference between enemy getting to target/player & then attack
        _attackTimer = waitBeforeAttack;
        // store chase distance to be used at different points
        _currentChaseDistance = chaseDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (_enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (_enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }
    void Patrol()
    {
        // start moving
        _navAgent.isStopped = false; // start the navagentwww
        _navAgent.speed = walkSpeed;

        _patrolTimer += Time.deltaTime; // additive to patrol timer
        if (_patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            _patrolTimer = 0f; // reset timer so will continue to loop through new points

        }

        if (_navAgent.velocity.sqrMagnitude > 0) // if enemy moving
        {
            _enemyAnim.Walk(true);
        }
        else
        {
            _enemyAnim.Walk(false);
        }

        // test chase distance <= enemy position - player position 
        if (Vector3.Distance(transform.position, _targetTF.position) <= chaseDistance)
        {
            _enemyAnim.Walk(false); // turn off walk animation
            _enemyState = EnemyState.CHASE;

            // Play AUDIO after spotted / attacked by player at range
            _enemyAudio.PlayScreamSound();

        }
    }

    void SetNewRandomDestination()
    {
        // create a random target spot to patrol based on set radius min/max
        float _randomRadius = Random.Range(patrolRadiusMin, patrolRadiusMax); // random radius from predetermined Min/Max distances

        Vector3 _randomDirection = Random.insideUnitSphere * _randomRadius; // random point on globe using the random radius/distance
        _randomDirection += transform.position; // make enemy's position by adding it to current position

        // check if _randomDirection is navigable / inside map bounds, and store point inside bounds info via out navmeshHit. only returns a naviagble point
        NavMeshHit navmeshHit;
        NavMesh.SamplePosition(_randomDirection, out navmeshHit, _randomRadius, -1); // -1 = check on all layers (in case a mask is applied)

        // set nav agent's destination
        _navAgent.SetDestination(navmeshHit.position);


    }

    void Chase()
    {
        // start moving
        _navAgent.isStopped = false;
        _navAgent.speed = runSpeed;
        // target / player position set as endpoint ("chasing" after player)
        _navAgent.SetDestination(_targetTF.position);

        // run to player
        if (_navAgent.velocity.sqrMagnitude > 0) // if enemy moving
        {
            _enemyAnim.Run(true);
        }
        else
        {
            _enemyAnim.Run(false);
        }

        // check distance for attack
        if (Vector3.Distance(transform.position, _targetTF.position) <= attackDistance)
        {
            // within distance

            // stop run/walk animation & change state
            _enemyAnim.Run(false);
            _enemyAnim.Walk(false);
            _enemyState = EnemyState.ATTACK;

            // reset chase distance to stored value, if changed after player attack enemy at range
            if (chaseDistance != _currentChaseDistance)
            {
                chaseDistance = _currentChaseDistance;
            }
        }
        // player runs out of range
        else if (Vector3.Distance(transform.position, _targetTF.position) > chaseDistance)
        {
            // outside distance

            // stop running & reset state to patrol
            _enemyAnim.Run(false);
            _enemyState = EnemyState.PATROL;
            //reset patrol timer for new position immediately
            _patrolTimer = patrolForThisTime;
            // reset chase distance to stored value, if changed after player attack enemy at range
            if (chaseDistance != _currentChaseDistance)
            {
                chaseDistance = _currentChaseDistance;
            }
        }
    }

    void Attack()
    {
        // stop moving / stop navagent
        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = (true);

        // attack timer, for pauses in between attacks
        _attackTimer += Time.deltaTime;
        if (_attackTimer > waitBeforeAttack)
        {
            // attack animation
            _enemyAnim.Attack();
            // reset attack timer
            _attackTimer = 0f;

            // attack AUDIO
            _enemyAudio.PlayAttackSound();

        }
        // test player distance, if player runs away : enemy position - player position > attack distanc + chaseAfterAttackDistance
        if (Vector3.Distance(transform.position, _targetTF.position) > (attackDistance + chaseAfterAttackDistance))
        {
            // chase after attack distance gives player space buffer so chase does not start right away
            _enemyState = EnemyState.CHASE;
        }

    }
    // attackpoint methods copied from player attack script
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

    // enemy state method - must be accessible / public
    public EnemyState EnemyState { get; set; }

}

