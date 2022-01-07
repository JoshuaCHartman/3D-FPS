using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyState { PATROL, CHASE, ATTACK}

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

    private Transform _targetTF;

    private void Awake()
    {
        // get references
        _enemyAnim = GetComponent<EnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();

        _targetTF = GameObject.FindWithTag(Tags.PLAYER_TAG).transform; // get players coordinates

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
        if(_enemyState == EnemyState.PATROL)
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
        _navAgent.isStopped = false; // start the navagent
        _navAgent.speed = walkSpeed;

        _patrolTimer += Time.deltaTime; // additive to patrol timer
        if (_patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            _patrolTimer = 0f;
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
        throw new NotImplementedException();
    }

    void Attack()
    {
        throw new NotImplementedException();
    }

   
}
