using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPool : MonoBehaviour
{
    public int poolSize;

    public GameObject cubeEnemy;
    public GameObject sphereEnemy;
    public GameObject triangleEnemy;



    public List<GameObject> inactiveCube= new List<GameObject>();
    public List<GameObject> inactiveTriangle = new List<GameObject>();
    public List<GameObject> inactiveSphere = new List<GameObject>();
    public List<GameObject> activeEnemy = new List<GameObject>();

   
    

    private void Awake()
    {
        
       for(int i=0;i<poolSize;i++)
        {
            MakeNewCube();
            MakeNewSphere();
            MakeNewTriangle();
        }
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
            obj.SetActive(false);
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
        }
        activeEnemy.Clear();
    }

    public void StopCurrentActiveObj()
    {
        
    }
     

    




}
