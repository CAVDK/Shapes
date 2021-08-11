using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;
public class Enemy3D :moveObject
{
    public float enemySpeed;
    [SerializeField]
    Collider coll;
    public ParticleSystem ps;
   


    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider>();
     
     
        coll.isTrigger = true;
        //Debug.Log(coll.isTrigger);
        currentSpeed = enemySpeed;
       // moveDirection = new Vector3(Random.Range(1f, 9f), Random.Range(1f, 9f), 0f).normalized;
      //  MoveTheObject(moveDirection * forceAppliedToMove);
    }

    
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
       // Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {

            CameraShaker.Instance.ShakeOnce(3.5f, 3.5f, 0.2f, 0.5f);
            //onDeath Play a partical effect
            GameController.insatance.pool.activeEnemy.Remove(gameObject);
            if (gameObject.CompareTag( "cube"))
            {
                GameController.insatance.pool.inactiveCube.Add(gameObject);
               

            }
            else if(gameObject.CompareTag("sphere"))
            {
                GameController.insatance.pool.inactiveSphere.Add(gameObject);
            }
            else
            {
                GameController.insatance.pool.inactiveTriangle.Add(gameObject);
            }

              GameController.insatance.PlayParticalEffects(transform.position, ps.tag);

           

            gameObject.SetActive(false);
        }
    }




    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Trigger")
        {
            coll.isTrigger = false;
        }
    }
    
    public void MoveTheEnemy()
    {
       // MoveTheObject(moveDirection);
    }



}
