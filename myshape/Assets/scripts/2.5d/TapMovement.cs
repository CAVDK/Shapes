using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMovement : MonoBehaviour
{
    
    //refrences
    private Collider col;
    private Rigidbody rb;
    public Transform refObj;

    //adjustable variables
    public float tapForceX;
    public float tapForceY;

    public ForceMode forceMode;
    public float x = 0;
    public int dir = 0;
    public float maxVel;


    //jump adjuts ment
    bool jump;


    private void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        

       
    }
    private void FixedUpdate()
    {
        if(jump)
        {
            rb.AddForce(Vector3.up * tapForceY *x, forceMode);
            rb.AddForce(Vector3.right * tapForceX  * dir*x, forceMode);
        }
        


    }

}
