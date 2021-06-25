using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Mover
{
    [SerializeField]
    Sprite[] playerSprite;
    int i = 0;
    SpriteRenderer _spriteRenderer;
    public float refletedForce=100f;


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

    
    #endregion

    #region Functions

    void SpriteSwitch()
    {
       
        //changing the sprite of the player every time the player taps on sreem
        i++;
        if (i >= playerSprite.Length)
            i = 0;
        _spriteRenderer.sprite = playerSprite[i];
       // Debug.Log(_spriteRenderer.sprite.name);
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
        Vector3 pointOfcontact = collision.GetContact(0).point;
        Vector3 normalcontact = collision.GetContact(0).normal;

        if (collision.gameObject.tag == _spriteRenderer.sprite.name)
        {
            //do some artistic 
            //play sound
            //add a partical effect
            //increase the size and through it back again
            Debug.Log("Correct Hit");
            move(normalcontact.normalized);



        }
        else if (collision.gameObject.tag == "walls")
        {
            //move(Vector3.Reflect(pointOfcontact, normalcontact));
        }
        else if (collision.gameObject.tag == "Lava")
        {
            //shooot it in ransom direction or decide what you wna do
        }
        else
        {
            //it means the collision is wromg and we should end the game
        move(normalcontact.normalized);



        }
        Debug.DrawRay(transform.position, normalcontact*100f,Color.green);


    }

    protected override void move(Vector3 moveDir)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(moveDir* refletedForce);
    }



    #endregion


}
