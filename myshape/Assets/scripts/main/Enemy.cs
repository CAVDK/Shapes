using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    private  float size;
    public float minSize =0.5f, maxSize = 0.9f;
    public  Vector3 moveDirection;
   
  
   
    //refrence
    
    private Collider2D Col;
    

    protected override void Start()
    {
        base.Start();
        CalculateSize();
        CalculateSpeed(size);
        move(moveDirection);
        
        Col = GetComponent<Collider2D>();
             
    }

    protected override void Update()
    {
        base.Update();
    }


    //calculate random sprite size 
    private void CalculateSize()
    {
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size;
    }


    //calculate what should be the size of the enemy
    void CalculateSpeed(float _size)
    {
        //calculate the speed based on the size of the enemy 
        currentSpeed = speed * (  1 - (_size-0.5f)/ 0.45f);
    }


    //trigger is set while spawning and is set off once it enters the playing area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="_trigger")
        {
            var _colider = GetComponent<Collider2D>();
            _colider.isTrigger = false;
            Debug.Log(gameObject.name);
            Col.isTrigger = false;
            


        }
    }

    //triggerd when collided with player,walls or anyother enemy 
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.tag == "enemy")
        {
            CalculateDeflection(collision.GetContact(0).normal, collision.GetContact(0).point);
        }

        if(collision.gameObject.tag == "Player")
        {
            //for now just destroy itself
            Destroy(gameObject);
        }


    }

   

















}
