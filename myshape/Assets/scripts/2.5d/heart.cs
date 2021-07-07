using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : moveObject
{
    public float speed;
    public Collider coll;
  protected override void Start()
    {
        currentSpeed = speed;
        coll = GetComponent<Collider>();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("lava"))
        {
            gameObject.SetActive(false);
            
            GameController.insatance.pool.heals.Add(gameObject);
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag ( "Trigger"))
        {
            coll.isTrigger = false;
        }
    }
    private void FixedUpdate()
    {

       // MoveTheObject(moveDirection);
    }
    public void MoveTheEnemy()
    {
       // MoveTheObject(moveDirection);
    }

}
