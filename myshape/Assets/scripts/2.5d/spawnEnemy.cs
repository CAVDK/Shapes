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

    public float enmeySpeed = 5;
    public float enemySpeedMultiplier = 0.6f;

    public bool finisedSpawnning;

    public bool canSpawn = true;
    [SerializeField] Transform parentGameObj;

    public float incrementInForce;
    public float maxPlayerForce;

    EnemyPool pool;

    //power up spawnner
    // 1-  heal
    public float powerUpSpawnInterval;
    private float lastpowerSpawn;
    public float maxPowerUpSpawninterval;

    private void Awake()
    {
        pool = FindObjectOfType<EnemyPool>();
        lastpowerSpawn = Time.time;
    }
    private void Start()
    {
        InvokeRepeating("UpdateSpawnintervals", 45f, updateCredenialTime);
        lastpowerSpawn = Time.time;
        canSpawn = true;
        enemyCount = 1;
        currTime = Time.time - spawninterval;
        spawninterval = 3f;
        waveInterval = 4f;
        enemySpeedMultiplier = 0.5f;
    }
    private void OnEnable()
    {
        lastpowerSpawn = Time.time;
        currTime = Time.time;
        
    }

    private void UpdateSpawnintervals()
    {

        int i = Random.Range(0, 100);
        if (i < 50)
        {
            spawninterval -= 0.2f;
            waveInterval -= 0.3f;
        }
        else
        {
             enemySpeedMultiplier += 0.1f;
            if(enemySpeedMultiplier > 1.8f)
            {
                enemySpeedMultiplier = 1.8f;
            }
        }

        GameController.insatance.playerMovementScript.forceAppliedToMove += incrementInForce;
        if(GameController.insatance.playerMovementScript.forceAppliedToMove > maxPlayerForce)
        {
            GameController.insatance.playerMovementScript.forceAppliedToMove = maxPlayerForce;
        }

        enemyCount = (int)(Time.timeSinceLevelLoad / 60f);
        if (enemyCount > maxEnemyCount) enemyCount = maxEnemyCount;
        
    }

    private void Update()
    {
        if(Time.time - lastpowerSpawn > powerUpSpawnInterval)
        {
            SpawnPowerUps();
            lastpowerSpawn = Time.time;
            powerUpSpawnInterval = Random.Range(5f, maxPowerUpSpawninterval);

        }
        if (!canSpawn) return;
        if(Time.time - currTime > waveInterval  )
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
        int i = Random.Range(1, 500);
        int count=1;
         if(i<70 && i>60)
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
       
       
        if (pool.activeEnemy.Count == 0 && !finisedSpawnning)
            yield return new WaitForSeconds(1f);
        else
        yield return new WaitForSeconds(indiavideualSpawnInterval);
        int i = Random.Range(0, enmeyPrefab.Length);
        int j = Random.Range(0, spawnLocation.Length);
        GameObject newEnemy = pool.GetInActiveEnemy(enmeyPrefab[i].tag);
        newEnemy.transform.parent = parentGameObj;

        newEnemy.transform.position = spawnLocation[j].position;
        //newEnemy.GetComponent<Enemy3D>().enemySpeed = enmeySpeed * enemySpeedMultiplier;
        newEnemy.GetComponent<Enemy3D>().moveDirection = new Vector3(Random.Range(minTarPos.x, maxTarPos.x),
            Random.Range(minTarPos.y, maxTarPos.y), 0f) - spawnLocation[j].position;
        newEnemy.GetComponent<Collider>().isTrigger = true;
        newEnemy.SetActive(true);
        newEnemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SpawnPowerUps()
    {
        int i = Random.Range(0, 9999);
        if (i < 5000)
       {
            //randomly spawn a powerup
            //heals
            if (pool.heals.Count == 0) pool.GenrateHeals();
            int j = Random.Range(0, spawnLocation.Length);
            GameObject newPowerup = pool.heals[0];
            heart newHeart = newPowerup.GetComponent<heart>();
            pool.activePowerups.Add(newPowerup);
            pool.heals.Remove(newPowerup);
            newPowerup.transform.position = spawnLocation[j].position;
         //   newHeart.currentSpeed *= enemySpeedMultiplier;
            ;
            newHeart.moveDirection = new Vector3(Random.Range(minTarPos.x, maxTarPos.x), Random.Range(minTarPos.y, maxTarPos.y), 0f)
    - spawnLocation[j].position;

            newPowerup.GetComponent<Collider>().isTrigger = true;
            newPowerup.SetActive(true);


       }
    }





}
