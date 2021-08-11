using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public bool isRewinding;
    public List<Vector3> positionsData ;

    private void Start()
    {
        positionsData = new List<Vector3>();
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartRewinding();
        }
        if(Input.GetKeyUp(KeyCode.Return))
        {
            StopRewinding();
        }


    }

    public void FixedUpdate()
    {
        if(isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if(positionsData.Count>0)
        {
            transform.position = positionsData[0];
            positionsData.RemoveAt(0);
        }
        else
        {
            StopRewinding();
        }
        

    }
    private void Record()
    {
        positionsData.Insert(0,transform.position);
    }



    public  void StartRewinding()
    {
        isRewinding = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void StopRewinding()
    {
        isRewinding = false;
        GetComponent<Rigidbody>().isKinematic = false;

    }


}
