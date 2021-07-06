using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public bool playerActive;
    
    [SerializeField]
    private int totalPlayerLife;
    public int playerLifeLeft;
    
    

    public GameObject player;

    [SerializeField]
    private spawnEnemy _spawnEnemy;
    public EnemyPool pool;

    public static GameController insatance { get; set; }

    public Animator resumeAndPause;
    public Animator deathAnimator;

    //future use maybe

    public  Button pauseButton;

    private void Awake()
    {
        insatance = this;
        _spawnEnemy = FindObjectOfType<spawnEnemy>();
        pool = FindObjectOfType<EnemyPool>();
        
    }
    private void Start()
    {
        playerLifeLeft = totalPlayerLife;
        _spawnEnemy.canSpawn = true;

    }


    public void Death()
    {
        pauseButton.interactable = false;
        deathAnimator.SetTrigger("death");
        Time.timeScale = 0f;
        player.SetActive(false);
        _spawnEnemy.canSpawn = false;
        FindObjectOfType<EnemyPool>().ClearActiveArray();
        //add a slow motion effect 
        //add a death window
        
       
        
    }

    public void ResumeClicked()
    {
        _spawnEnemy.canSpawn = true;
        Time.timeScale = 1f;
        resumeAndPause.SetTrigger("resumePressed");
    }

    public void PauseClicked()
    {
        _spawnEnemy.canSpawn = false;
        Time.timeScale = 0f;
        resumeAndPause.SetTrigger("pausePressed");
    }

    public void Home()
    {
        Time.timeScale = 1f;//pause menu makes the timeline to 0 
        SceneManager.LoadScene(0);//home scene
    }

    





}
