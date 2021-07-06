using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
   public Rigidbody rb;
    public Vector3 moveDirection;
    public float currentSpeed;
    public float forceAppliedToMove;
    public float bounceMultiplier;
   

   protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    protected virtual void Update()
    {
        //just so that my velocity does no goo burrrrr
        rb.velocity = rb.velocity.normalized * currentSpeed*Time.deltaTime;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        transform.position = currentPosition;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //check if the collideing onject is a enemy 
        Vector3 collisonNormal = collision.GetContact(0).normal;
        Vector3 collisonPosition = collision.GetContact(0).point;
        Vector3 newDirection = GetCollisionReflection(collisonNormal, collisonPosition);
        moveDirection = newDirection;
        MoveTheObject(newDirection*forceAppliedToMove*bounceMultiplier);
    }

    

  protected virtual  Vector3 GetCollisionReflection(Vector3 normal ,Vector3 position)
    {
        Vector3 newDirection = Vector3.Reflect(moveDirection, normal);
        moveDirection.z = 0f;
        return newDirection;
    }

    protected virtual void MoveTheObject(Vector3 directionTomove)
    {
        rb.velocity = Vector3.Lerp(rb.velocity.normalized, Vector3.zero, Time.deltaTime);
        rb.AddForce(directionTomove.normalized * forceAppliedToMove);
    }

}
