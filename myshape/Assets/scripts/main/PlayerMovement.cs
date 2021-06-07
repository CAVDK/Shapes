using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Sprite[] playerSprite;
    int i = 0;
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigidBody;

    //playerMovemnt
    public float speed =5f;
    float currentSpeed;
   public ForceMode2D _foreceMode = ForceMode2D.Force;

    public float moveForce = 10f;

    //unity methods
    #region unity methods
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
        
        
    }

    private void Update()
    {
        //applying a constant speed to the player we can modify it later
        _rigidBody.velocity = _rigidBody.velocity.normalized * currentSpeed * Time.deltaTime;


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
    }

    void MovePlayer()
    {
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveDirection = tapPosition - transform.position;//get the direction to move in 
        _rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity,Vector3.zero,Time.deltaTime);
        _rigidBody.AddForce(moveDirection * moveForce, _foreceMode);


    }

    #endregion


}
