using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwnEnemy : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] objectToSpawn;
    public float timeBetweenspawn;

    private void Start()
    {
        InvokeRepeating("StartSpawnning", 1, timeBetweenspawn);
    }

   void StartSpawnning()
    {
        for(int i=0;i<objectToSpawn.Length;i++)
        {
            if(Random.Range(0,100)<20)
            {
                //spawn
                int j =  Random.Range(0, objectToSpawn.Length);
                Instantiate(objectToSpawn[j], spawnPoints[i].position, Quaternion.identity);
            }
        }


        
    }


}
