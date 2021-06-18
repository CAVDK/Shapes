using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    private  float size;
    public float minSize =0.5f, maxSize = 0.9f;
    public  Vector3 moveDirection;

    protected override void Start()
    {
        base.Start();
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size;
        
        CalculateSpeed(size);
             
    }

    void CalculateSpeed(float _size)
    {
        //calculate the speed based on the size of the enemy 
        currentSpeed = speed * (  1 - (0.9f -_size)/ 0.55f);
    }
   public void  JustSpawnned(Vector3 dir)
    {
        move(dir);
    }

















}
