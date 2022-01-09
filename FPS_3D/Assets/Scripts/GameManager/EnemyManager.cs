using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private GameObject _boarPrefab, _cannibalPrefab;

    public Transform[] cannibalSpawnPointsTF, boarSpawnPointsTF; // spawn ppoint arrays (TF denotes transfrom)

    [SerializeField] private int _cannibalEnemyCount, _boarEnemyCount; // how many enemies in game
    private int _initialCannibalCount, _initialBoarCount; // initial stored enemy totals
    public float waitBeforeSpawnEnemiesTime = 10f;

    
    void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        _initialBoarCount = _boarEnemyCount;
        _initialCannibalCount = _cannibalEnemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");

    }

    void SpawnEnemies()
    {
        SpawnCannibal();
        SpawnBoar();
    }

    private void SpawnBoar()
    {
        int index = 0; // index for array of spawn points
        for (int i = 0; i < _boarEnemyCount; i++)
        {
            // check to avoid out of range - works bc enemy numbers and spawn points are the same
            if (index >= boarSpawnPointsTF.Length)
            {
                index = 0;
            }
            // instantiate a boar prefab, at the coordinates of the spawn point at index #, with default rotation of prefab
            Instantiate(_boarPrefab, boarSpawnPointsTF[index].position, Quaternion.identity);
            index++;
        }
        _boarEnemyCount = 0;
    }

    private void SpawnCannibal()
    {
        int index = 0; // index for array of spawn points
        for (int i = 0; i< _cannibalEnemyCount; i++)
        {
            // check to avoid out of range - works bc enemy numbers and spawn points are the same
            if (index>= cannibalSpawnPointsTF.Length)
            {
                index = 0;
            }

            Instantiate(_cannibalPrefab, cannibalSpawnPointsTF[index].position, Quaternion.identity);
            index++;
        }
        _cannibalEnemyCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawnEnemiesTime);
        SpawnCannibal();
        SpawnBoar();

        StartCoroutine("CheckToSpawnEnemies"); //nested - will results in constant enemy spawning until player dies
    }

    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            _cannibalEnemyCount++;
            if (_cannibalEnemyCount > _initialCannibalCount)
            {
                _cannibalEnemyCount = _initialCannibalCount;
            }
        }
        else
        {
            _boarEnemyCount++;
            if (_boarEnemyCount > _initialBoarCount)
            {
                _boarEnemyCount = _initialBoarCount;
            }
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StopSpawningEnemies() // trigger when player dies
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
