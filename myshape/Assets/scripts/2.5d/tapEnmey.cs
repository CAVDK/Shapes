using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapEnmey : MonoBehaviour
{
    private Rigidbody rb;
    public float enemyspeed;
    private Collider col;
    public bool noTrigger;
    public Vector3 moveDir;
    public float maxSpeed;
    public float maxForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
       //noTrigger = true;
        col.isTrigger = noTrigger;
        moveDir = new Vector3(Random.Range(0, -2f), Random.Range(-1f, 1f), 0f) - transform.position ;

    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir * maxForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            moveDir = Vector3.Reflect(moveDir, collision.GetContact(0).normal);

        }

    }
}
