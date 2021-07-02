﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D :moveObject
{
    public float enemySpeed;
    [SerializeField]
    Collider coll;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider>();

        coll.isTrigger = true;
        Debug.Log(coll.isTrigger);
        currentSpeed = enemySpeed;
       // moveDirection = new Vector3(Random.Range(1f, 9f), Random.Range(1f, 9f), 0f).normalized;
      //  MoveTheObject(moveDirection * forceAppliedToMove);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Trigger")
        {
            coll.isTrigger = false;
        }
    }
    private void FixedUpdate()
    {
        MoveTheObject(moveDirection);
    }
    public void MoveTheEnemy()
    {
        MoveTheObject(moveDirection);
    }



}