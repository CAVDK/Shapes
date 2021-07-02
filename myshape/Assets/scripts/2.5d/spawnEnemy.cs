using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject[] enmeyPrefab;
    public Transform[] spawnLocation;
    public Vector2 minTarPos;
    public Vector2 maxTarPos;

    public int enemyCount =1;
    public int maxEnemyCount;
    //
    public float waveInterval;
    private float currTime;
    //
    public float spawninterval;
    public float maxSpawninterval;
    public float indiavideualSpawnInterval;
    //
    public float updateCredenialTime;

    public float enemySpeedMultiplier = 0.8f;

    public bool finisedSpawnning;
    private void Start()
    {


        InvokeRepeating("UpdateSpawnintervals", 45f, updateCredenialTime);
    }

    private void UpdateSpawnintervals()
    {

        int i = Random.Range(0, 100);
        if (i < 50)
        {
            spawninterval -= 0.2f;
        }
        else
        {
            enemySpeedMultiplier += 0.2f;
            if(enemySpeedMultiplier > 1.8f)
            {
                enemySpeedMultiplier = 1.8f;
            }
        }
        enemyCount = (int)(Time.timeSinceLevelLoad / 60f);
        if (enemyCount > maxEnemyCount) enemyCount = maxEnemyCount;
        
    }

    private void Update()
    {
        if(Time.time - currTime > waveInterval)
        {
            if(finisedSpawnning)
            {
                StartCoroutine("SpawnNow");
                currTime = Time.time;
            }
            
        }
        
    }

    IEnumerator SpawnNow()
    {
        finisedSpawnning = false;
        int i = Random.Range(1, 1000);
        int count=1;
        if(i<10)
        {
            count = enemyCount;
        }else if(i>100 && i<50)
        {
            count = 3;
        }else if(count>100 && count<300)
        {
            count = 2;
        }
        else
        {
            count = 1;
        }

        for(int j=0;j<count;j++)
        {
            StartCoroutine("SpawnIt");
        }
        yield return new WaitForSeconds(spawninterval);
        finisedSpawnning = true;

        
    }

    IEnumerator SpawnIt()
    {
        //instantialte the enemy
        int i = Random.Range(0, enmeyPrefab.Length);
        int j = Random.Range(0, spawnLocation.Length);

        GameObject newEnemy = Instantiate(enmeyPrefab[i], spawnLocation[j].position, Quaternion.identity);
        newEnemy.GetComponent<Enemy3D>().enemySpeed *= enemySpeedMultiplier;
        newEnemy.GetComponent<Enemy3D>().moveDirection = new Vector3(Random.Range(minTarPos.x,maxTarPos.x),Random.Range(minTarPos.y,maxTarPos.y ),0f) 
            -spawnLocation[i].position;
        newEnemy.GetComponent<Collider>().isTrigger = true;

        yield return new WaitForSeconds(indiavideualSpawnInterval);
    }





}
