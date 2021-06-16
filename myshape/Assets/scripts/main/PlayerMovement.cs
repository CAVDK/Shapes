using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Mover
{
    [SerializeField]
    Sprite[] playerSprite;
    int i = 0;
    SpriteRenderer _spriteRenderer;

    //unity methods
    #region unity methods
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        //changing shape
        if (Input.GetButtonDown("Fire1"))
        {

            SpriteSwitch();
            MovePlayer();

        }
    }

    private void FixedUpdate()
    {
       

    }
    #endregion

    #region Functions

    void SpriteSwitch()
    {
       
        //changing the sprite of the player every time the player taps on sreem
        i++;
        if (i >= playerSprite.Length)
            i = 0;
        _spriteRenderer.sprite = playerSprite[i];
        Debug.Log(_spriteRenderer.sprite.name);
    }

    void MovePlayer()
    {
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//get the point on screen where the user tapped on screen 
        Vector3 moveDirection = tapPosition - transform.position;//get the direction to move in 
        rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,Time.deltaTime);
        move(moveDirection);
       


    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    #endregion


}
