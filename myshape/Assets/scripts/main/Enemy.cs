using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    private  float size;
    public float minSize =0.5f, maxSize = 0.9f;
    private Vector3 moveDirection;

    protected override void Start()
    {
        base.Start();
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size;
        moveDirection = new Vector3(1f, -1f, 0f).normalized;
        move(moveDirection);
        CalculateSpeed(size);
             
    }
    void CalculateSpeed(float _size)
    {
        currentSpeed = speed * (  1 - (0.9f -_size)/ 0.55f);
    }

















}
