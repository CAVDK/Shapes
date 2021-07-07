using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemnt3d : moveObject
{
    public float speed;
    public GameObject[] playerBody;
    int indexOfplayer =0;
    public string playerName;
    public LayerMask enemyMask;
    
    
    
    
    protected override void Start()
    {
        base.Start();
        currentSpeed = speed;
        playerName = playerBody[indexOfplayer].name;
    }

    protected override void Update()
    {
        base.Update();
       

        if (Input.GetButtonDown("Fire1"))
        {
            UpdatePlayerBody();//update the name look of the player i.e change the shape
            Vector2 tapPosiiton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // Debug.Log(tapPosiiton);
            Vector3 newMovePos = new Vector3(tapPosiiton.x, tapPosiiton.y, 0);

            moveDirection = newMovePos - transform.position;
            moveDirection.z = 0f;
           

        }
       

       
    }

    private void FixedUpdate()
    {
        //MoveTheObject(moveDirection);
    }

    private void UpdatePlayerBody()
    {
        int lastIndex = indexOfplayer;
        indexOfplayer++;
        if(indexOfplayer>=playerBody.Length)
        {
            indexOfplayer = 0;
        }
        playerBody[indexOfplayer].SetActive(true);
        playerBody[lastIndex].SetActive(false);
        playerName = playerBody[indexOfplayer].name;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
       //player heal
        if(collision.gameObject.CompareTag("heal"))
        {
            //Debug.Log("heal");
            GameController.insatance.playerLifeLeft++;
            GameController.insatance.UpdateHealth();
            if (GameController.insatance.playerLifeLeft>3)
            {
                GameController.insatance.playerLifeLeft = 3;
                
            }
           

        }
        else 
        if (collision.gameObject.layer.ToString() == "9" )//9th layer is the enemy layer
        {
            
            if(!collision.gameObject.CompareTag(playerName))
            {
                //vall game manager
                // Debug.Log("wrong");
               // Debug.Log("hurt");
                GameController.insatance.playerLifeLeft--;
                GameController.insatance.UpdateHealth();
                if(GameController.insatance.playerLifeLeft<=0)
                {
                    GameController.insatance.Death();
                }
                //Debug.Log(GameController.insatance.playerLifeLeft);
                
            }
            else
            {
                GameController.insatance.UpdateScore();
            }
        }

       
        
    }

   

}
