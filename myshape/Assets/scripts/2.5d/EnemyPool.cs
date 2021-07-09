using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPool : MonoBehaviour
{
    public int poolSize;
    public Transform parents;

    public GameObject cubeEnemy;
    public GameObject sphereEnemy;
    public GameObject triangleEnemy;
    public GameObject heal;



    public List<GameObject> inactiveCube= new List<GameObject>();
    public List<GameObject> inactiveTriangle = new List<GameObject>();
    public List<GameObject> inactiveSphere = new List<GameObject>();
    public List<GameObject> activeEnemy = new List<GameObject>();
    public List<GameObject> heals = new List<GameObject>();
    public List<GameObject> activePowerups = new List<GameObject>();
   
    

    private void Awake()
    {
        
       for(int i=0;i<poolSize;i++)
        {
            MakeNewCube();
            MakeNewSphere();
            MakeNewTriangle();
        }
        GenrateHeals();
    }
    private void OnEnable()
    {
        ClearActiveArray();
        ClearPowerUpsArray();
    }


    private void Update()
    {
       
        
    }
    private void MakeNewCube()
    {
        MakeNewEnemy(cubeEnemy);
        
    }

    private void MakeNewSphere()
    {
        MakeNewEnemy(sphereEnemy);
    }

    private void MakeNewTriangle()
    {
        MakeNewEnemy(triangleEnemy);
    }

    public  void GenrateHeals()
    {
       for(int i=0;i<2;i++)
        {
            GameObject healObj = Instantiate(heal, transform.position, Quaternion.identity);
            healObj.transform.parent = parents;
            healObj.SetActive(false);
            heals.Add(healObj);

        }
    }

    private void MakeNewEnemy(GameObject enemyPrefab)
    {
        GameObject obj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        obj.SetActive(false);

        switch(enemyPrefab.tag)
        {
            case "cube" :
                inactiveCube.Add(obj);
                break;

            case "triangle":
                inactiveTriangle.Add(obj);
                break;

            case "sphere":
                inactiveSphere.Add(obj);
                break;
        }
    }

    public GameObject GetInActiveEnemy(string enemyName)
    {
        GameObject newObj = null ;
        switch (enemyName)
        {
            case "cube":
               
                if(inactiveCube.Count == 0)
                {
                    MakeNewCube();         
                }
                newObj = inactiveCube[0];
                inactiveCube.RemoveAt(0);
                break;

            case "triangle":

                 if(inactiveTriangle.Count==0)
                {
                    MakeNewTriangle();
                }
                newObj = inactiveTriangle[0];
                inactiveTriangle.RemoveAt(0);
                break;

            case "sphere":

                if(inactiveSphere.Count==0)
                {
                    MakeNewSphere();
                }
                newObj = inactiveSphere[0];
                inactiveSphere.RemoveAt(0);
                break;
        }
        activeEnemy.Add(newObj);
        
        return newObj;
    }

    public void ClearActiveArray()
    {
        foreach(GameObject obj in activeEnemy)
        {
            
            if(obj.tag == "cube")
            {
                inactiveCube.Add(obj);
            }else if(obj.tag == "sphere")
            {
                inactiveSphere.Add(obj);
            }
            else
            {
                inactiveTriangle.Add(obj);
            }
            obj.SetActive(false);
        }
        activeEnemy.Clear();
    }

    public void ClearPowerUpsArray()
    {
        foreach (GameObject go in activePowerups)
        {
            go.SetActive(false);
            heals.Add(go);
            activePowerups.Remove(go);
        }
    }
    
     

    




}
