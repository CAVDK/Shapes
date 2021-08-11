using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float spawnInterval;
    public float waveInterval;

    public float decrementInInterval;

    public bool canSpanw;
    [SerializeField] float spawnStartTime;
    [SerializeField] int activeSpanCount;
    [SerializeField] int maxSpawmCount;
    private void Start()
    {
        canSpanw = false;
        ResetSpawns();
        StartCoroutine("BeginSpawnning");
    }

    private void Update()
    {
        if (!canSpanw) return;

    }



    IEnumerator BeginSpawnning()
    {
        yield return new WaitForSeconds(spawnStartTime);
        Debug.Log("Start spawning ");
        canSpanw = true;
    }

    private void StartSpawnning()
    {

    }

    private void ResetSpawns()
    {

    }


}
