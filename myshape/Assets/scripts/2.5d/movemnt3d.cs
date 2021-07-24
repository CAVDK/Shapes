using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemnt3d : moveObject
{
    public float speed;
    public GameObject[] playerBody;
    public MeshRenderer[] playerMesh;
  
    int indexOfplayer =0;
    public string playerName;
    public LayerMask enemyMask;

    public float inputInterval;
    public bool canTakeInput = true;

   
    
    public Material inputAvaliableMaterial;


    
    public Material inputUnAvaliableMaterial;

    //partical effect
    [SerializeField]ParticleSystem _touch;
    [SerializeField] ParticleSystem _touch_1;




    private void Awake()
    {
        SetPlayeMaterial(inputAvaliableMaterial);
    }
    protected override void Start()
    {
        base.Start();
        currentSpeed = speed;
        playerName = playerBody[indexOfplayer].name;
        canTakeInput = true;
       
    }

    protected override void Update()
    {
        base.Update();
       

        if (Input.GetButtonDown("Fire1"))
        {
            UpdatePlayerBody();
            if (!canTakeInput)
            {
                
                return;

            }
            //set material to non input material;
            SetPlayeMaterial(inputUnAvaliableMaterial);
           //update the name look of the player i.e change the shape
            Vector2 tapPosiiton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Debug.Log(tapPosiiton);
            Vector3 newMovePos = new Vector3(tapPosiiton.x, tapPosiiton.y, 0);

            moveDirection = newMovePos - transform.position;
            moveDirection.z = 0f;
            canTakeInput = false;
            StartCoroutine("TakeInput");
            //Invoke("TakeInput", inputInterval);

        }
       

       
    }

    IEnumerator TakeInput()
    {
        yield return new WaitForSeconds(inputInterval);
        SetInputtoTrue();
        //set material to can take input;
        
    }

    void SetInputtoTrue()
    {
        canTakeInput = true;
        SetPlayeMaterial(inputAvaliableMaterial);
    }
    void SetPlayeMaterial(Material mat)
    {
        for (int i = 0; i < playerMesh.Length; i++)
        {
            playerMesh[i].sharedMaterial = mat;
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
                
                GameController.insatance.playerLifeLeft--;
                GameController.insatance.UpdateHealth();
                if(GameController.insatance.playerLifeLeft<=0)
                {
                    GameController.insatance.Death();
                }
                
                
            }
            else
            {
                GameController.insatance.UpdateScore();
            }
            StopCoroutine(TakeInput());
            SetInputtoTrue();
        }

        if(collision.gameObject.CompareTag("walls") )
        {
            if(_touch.isEmitting)
            {
                _touch_1.gameObject.transform.position = collision.GetContact(0).point;
                _touch_1.gameObject.SetActive(true);
                if(canTakeInput)
                {
                    //set the matarial to the current material
                    _touch_1.GetComponent<Renderer>().material = inputAvaliableMaterial;
                }
                else
                {
                    _touch_1.GetComponent<Renderer>().material = inputUnAvaliableMaterial ;

                }
                _touch_1.Play();
                
                StartCoroutine("ResetPartical",_touch_1);

                ;
            }
            else
            {
                if (canTakeInput)
                {
                    //set the matarial to the current material
                    _touch.GetComponent<Renderer>().material = inputAvaliableMaterial;
                }
                else
                {
                    _touch.GetComponent<Renderer>().material = inputUnAvaliableMaterial;

                }

                _touch.gameObject.transform.position = collision.GetContact(0).point;

                _touch.gameObject.SetActive(true);

                _touch.Play();
                StartCoroutine("ResetPartical",_touch);
            }
            
            

        }
       
        
    }

    IEnumerator ResetPartical(ParticleSystem ps)
    {
        yield return new WaitForSeconds(0.7f);
        ResetTheParticales(ps);
    }

   void ResetTheParticales(ParticleSystem ps)
    {
        ps.Stop();
        ps.Clear();
        ps.gameObject.SetActive(false);
        
    }
 

   

}
