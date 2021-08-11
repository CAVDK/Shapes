using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public bool playerActive;
    
    [SerializeField]
    private int totalPlayerLife;
    public int playerLifeLeft;

    public GameObject[] healthIcons;
    

    public GameObject player;
    public movemnt3d playerMovementScript;

    [SerializeField]
    private spawnEnemy _spawnEnemy;//remove it
    public EnemyPool pool;
   

    public static GameController insatance { get; set; }//abstraction

    public Animator resumeAndPause;
    public Animator deathAnimator;

    //player score
    public int playerScore =0 ;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI deathscore;


    public ParticleSystem cubePartical;
    public ParticleSystem spherePartical;
    public ParticleSystem prismPartical;



    //future use maybe

    public bool gameEnd = false;
    public float slowDownfactor;


    private void Awake()
    {
        insatance = this;
        _spawnEnemy = FindObjectOfType<spawnEnemy>();//rmove it 
        pool = FindObjectOfType<EnemyPool>();
        

       
        
    }
    private void Start()
    {
        playerLifeLeft = totalPlayerLife;
        _spawnEnemy.canSpawn = true;
        scoreText.text = "0";
        gameEnd = false;
       

        

    }
  

    public void Death()
    {
        gameEnd = true;
        _spawnEnemy.canSpawn = false;
        deathscore.text = playerScore.ToString();
        player.SetActive(false);
       
        StartCoroutine("PlayerDeath");
        //Time.timeScale = 0f;
        //add a slow motion effect 
        //add a death window



    }
    IEnumerator PlayerDeath()
    {
        //Play partical effect
        
        Time.timeScale = 1 / slowDownfactor;
       
        yield return new WaitForSeconds(1 / slowDownfactor);
        Time.timeScale = 1f;
        
        deathAnimator.SetTrigger("death");





    }

    public void ResumeClicked()
    {
        Time.timeScale = 1f;
        _spawnEnemy.canSpawn = true;
        resumeAndPause.SetTrigger("resumePressed");
       
    }

    public void PauseClicked()
    {
        if (gameEnd) return;

        _spawnEnemy.canSpawn = false;       
        resumeAndPause.SetTrigger("pausePressed");
        Time.timeScale = 0f;
    }

    public void Home()
    {
        Time.timeScale = 1f;//pause menu makes the timeline to 0 
        SceneManager.LoadScene(0);//home scene
    }

    public void UpdateHealth()
    {
       for(int i=1;i<=3;i++)
        {
            if(i<=playerLifeLeft)
            {
                healthIcons[i - 1].SetActive(true);
            }else if(i>playerLifeLeft)
            {
                healthIcons[i - 1].SetActive(false);
            }
        }
    }

    public void UpdateScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

   public void PlayParticalEffects(Vector3 pos, string goname)
    {
        ParticleSystem psTemp = null;
        switch (goname)
        {
            case "cubeParticle":
                
                psTemp = Instantiate(cubePartical, pos, cubePartical.gameObject.transform.rotation);
                psTemp.Play(true);
                Destroy(psTemp.gameObject, 2f);
                break;
            case "sphereParticle":
                psTemp = Instantiate(spherePartical, pos, cubePartical.gameObject.transform.rotation);
                psTemp.Play(true);
                Destroy(psTemp.gameObject, 2f);
                break;
            case "prismParticle":
                psTemp = Instantiate(prismPartical, pos, cubePartical.gameObject.transform.rotation);
                psTemp.Play(true);
                Destroy(psTemp.gameObject, 2f);
                break;
        }
        
    }






}
