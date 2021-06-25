
using UnityEngine;

public class Mover : MonoBehaviour
{
    //movement
    public float speed;
    public float moveForce;//force to be applied when spawned
    protected float currentSpeed;
    public Vector3 moveDirections;

    //baic refrences
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;

    }

    protected virtual void Update()
    { 
        //limiting the player or enemy to go beyond a vertain velocity i.e limiting the veloity
        rb.velocity = rb.velocity.normalized * Time.deltaTime * currentSpeed;



    }
    
    protected virtual void move( Vector3 moveDir)
    {
        rb.AddForce(moveDir * moveForce);
    }

   protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 pointOfcontact = collision.GetContact(0).point;
        Vector3 normalcontact = collision.GetContact(0).normal;
        CalculateDeflection(normalcontact, pointOfcontact);
      
      
          
            //Debug.DrawRay(pointOfcontact, normalcontact * 100f, Color.white);
            //when ever collided with a wall calculate the deflectiona and reduce the velocity to zeo and then apply force
           

      
    }
    protected virtual void CalculateDeflection(Vector3 normal, Vector3 pointofcontact)
    {
       
        //calculate the vector to apply force in after collision
        Vector3 newMoveDirection = Vector2.Reflect(pointofcontact, normal);
        moveDirections = newMoveDirection;

        rb.AddForce(moveForce * newMoveDirection );

        //zero out the velocity
        
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 2f*Time.deltaTime);
        
        //apply force in tat direction
        move(newMoveDirection.normalized);
    }





}
