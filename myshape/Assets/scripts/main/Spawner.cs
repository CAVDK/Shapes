using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoits;
    public GameObject[] enemies;
   
    public float spawnWaveInterval=5f;
    private float maxSpawnWaveinterval =20f;
    public float spawnInterval=2f;

    [SerializeField] int maxSpawnCount=5;
    public int activeSpawnsCount=1;
    float currentTime;
    public bool finishSpawning =true;

    //increse the spawn interval after 30 sec because no of active spawn points will increase
    //increase the no of active spawn points untill te=hey reach 5;
    //but make sure to first finish spawning the earlier wave

    private void Start()
    {
        currentTime = Time.time-3f;
        //after every 20 sec increase the active spawn count by 1
        InvokeRepeating("IncreaseSpawnCount",20f,20f);
    }


    private void IncreaseSpawnCount()
    {
        //also increased the spawn intervall by 1.2 of the original one  between each enemy to be spawn
        spawnInterval +=activeSpawnsCount*0.2f;

        //increse the spawn time between waves
        if (spawnWaveInterval > maxSpawnWaveinterval) spawnWaveInterval = maxSpawnWaveinterval;
        spawnWaveInterval += activeSpawnsCount*0.6f; 

        //increased the spawn count after 20 sec
        if (activeSpawnsCount>maxSpawnCount)
        {
            activeSpawnsCount = maxSpawnCount;
            CancelInvoke("IncreaseSpawnCount");
        }
        activeSpawnsCount++;
    }
    private void Update()
    {
        
        
        if(Time.time-currentTime > spawnWaveInterval)
        {

            //only continue to spawn the next wave after finishing the aearlier one
            if(!finishSpawning)
            {
                return;
            }
            currentTime = Time.time;
            Spawn();

        }
        
    }




    public void  Spawn()
    {


        finishSpawning = false;
        for(int i=0;i<activeSpawnsCount;i++)
        {
            //takek any spawn point and then spawn 
            //wait for a spawn intervall before spawnning each enemy
            StartCoroutine("SpawnEnemy");
        }
        finishSpawning = true;
      
       
    }

    IEnumerator SpawnEnemy()
    {
        int i = Random.Range(0, enemies.Length);//enemy to spawn
        int j = Random.Range(0, spawnPoits.Length);//spawn position
        GameObject newEnemy = Instantiate(enemies[i], spawnPoits[j].position, Quaternion.identity);
        //expensive call
        Enemy _enemy = newEnemy.GetComponent<Enemy>();
        _enemy.moveDirection = new Vector3(Random.Range(-2f,2f),Random.Range(0f,3.5f),0f) - spawnPoits[j].position;
        
        

        yield return new WaitForSeconds(spawnInterval);
    }
}
