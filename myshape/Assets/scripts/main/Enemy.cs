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
       
        
        Col = GetComponent<Collider2D>();
             
    }

    protected override void Update()
    {
        transform.Translate(moveDirection.normalized * currentSpeed * Time.deltaTime);
        
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
        currentSpeed = speed * (  1 - (_size - 0.6f)/ 0.6f);
    }


    //trigger is set while spawning and is set off once it enters the playing area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="_trigger")
        {
            var _colider = GetComponent<Collider2D>();
            _colider.isTrigger = false;
           // Debug.Log(gameObject.name);
            Col.isTrigger = false;
            


        }
    }

    //triggerd when collided with player,walls or anyother enemy 
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 pointOfcontact = collision.GetContact(0).point;
        Vector3 normalcontact = collision.GetContact(0).normal;
        if (collision.gameObject.tag == "Player")
        {
            //for now just destroy itself
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == gameObject.layer)
        {
            moveDirection = -moveDirection;
            return;
        }

        
        
        CalculateDeflection(normalcontact, pointOfcontact);








    }

    protected override void CalculateDeflection(Vector3 normal, Vector3 pointofcontact)
    {
       
       
            Vector3 temp_newMoveDir = Vector2.Reflect(pointofcontact, normal);
        if (temp_newMoveDir.magnitude<0.5f)
        {
            moveDirection = new Vector3(Random.Range(-2f, 2f), Random.Range(-3f, 3f), 0f);
            return;
        }
        moveDirection = temp_newMoveDir;



    }






















}
