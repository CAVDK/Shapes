using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;

    public float spawnInterval;
    public float waveInterval;
    public float updateInterval;
    public int enemySpawnCount;
    public int maxEnemySpawncount;
    private float currentTime;
    public Vector2 minTargetPos,maxTargetPos;

    enum SpawnState {
        spawnningStart,spawnning,spawningover
        };
    SpawnState spawnState = SpawnState.spawningover;
    

    //after every 30 sec we have increment the o of active spawn count by one  
    private void Start()
    {
        InvokeRepeating("IncrementIntervalAndactivePointCount", 5f, updateInterval);
    }

    private void Update()
    {
        if(Time.time-currentTime >waveInterval)
        {
            if (spawnState == SpawnState.spawningover)
            {
                currentTime = Time.time;
                StartCoroutine("SpawnObj");
            }
        }
        
    }
    public Vector3 NewMoveDirectionGrnrator()
    {
        Vector3 newDirecttn = new Vector3(Random.Range(minTargetPos.x, maxTargetPos.x), Random.Range(minTargetPos.y, maxTargetPos.y), 0f);
        return newDirecttn;
    }

    private void IncrementIntervalAndactivePointCount()
    {
        enemySpawnCount++;
        waveInterval += 2f;
        spawnInterval -= enemySpawnCount * 0.3f;
        if(enemySpawnCount>maxEnemySpawncount)
        {
            enemySpawnCount = maxEnemySpawncount;
            CancelInvoke("IncrementIntervalAndactivePointCount");
        }


    }
    IEnumerator  SpawnObj()
    {
        spawnState = SpawnState.spawnning;
        for (int i = 0; i < enemySpawnCount; i++)
        {
            int k = Random.Range(0, enemies.Length);//enemy to spawn
            int j = Random.Range(0, spawnPoints.Length);//spawn position
            //Debug.Log(activeSpawnsCount + " -" + Time.time +" "+i);
            yield return new WaitForSeconds(spawnInterval);
            //Debug.Log(activeSpawnsCount + "__" +Time.time+" "+i);
            GameObject newEnemy = Instantiate(enemies[k], spawnPoints[j].position, Quaternion.identity);

            

            Enemy3D _enemy = newEnemy.GetComponent<Enemy3D>();
            _enemy.moveDirection = NewMoveDirectionGrnrator() - spawnPoints[j].position;
            


        }
        spawnState = SpawnState.spawningover;
    }



}
