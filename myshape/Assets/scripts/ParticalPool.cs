using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct Particle
{
    public ParticleSystem _deathParticle;
    public string name;
    public int index;
}

[System.Serializable]
public struct ParticleList
{
    public List<ParticleSystem> particleList;
}
public class ParticalPool : MonoBehaviour
{
    
    public Transform _Parent;
   

    
    public List<Particle> _DeathParticles = new List<Particle>();//the prefabs of the particle


    public List<List<ParticleSystem>> _InactiveParticleEffectsPool;
   
    

    [SerializeField] int _PoolSize = 3;//set the initial pool size to 4 i.e total 12 game objets
    [SerializeField] int _OverFlowSize = 2;
 

    #region SingleTon and Intitialing Pool
    public static ParticalPool instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
           
        }
        instance = this;
        _InactiveParticleEffectsPool = new List<List<ParticleSystem>>();
        InstantiatePool();
        

    }
    #endregion

    public void InstantiatePool()
    {
        for(int i=0;i < _DeathParticles.Count;i++ )
        {
            List<ParticleSystem> tempList = new List<ParticleSystem>();
             for(int j=0;j<_PoolSize;j++)
            {

                ParticleSystem ps= MakeAnyParticle(i);
                if ( ps!= null)
                {
                    tempList.Add(ps);
                }
                ps = null;
            }
            _InactiveParticleEffectsPool.Add(tempList);
            
            tempList.Clear();
        }
    }

    public ParticleSystem MakeAnyParticle(int _ParticleArrayNo)
    {
        if(_ParticleArrayNo >= _DeathParticles.Count)
        {
            Debug.LogError("Death Particle Count  "+ _DeathParticles.Count);
            Debug.LogError("You have Entered Wrong Partical No -- Particle No Out Of Bounds  " + _ParticleArrayNo);
            return null;
        }
        ParticleSystem ps = Instantiate(original: _DeathParticles[_ParticleArrayNo]._deathParticle, position: _Parent.position, Quaternion.identity, _Parent);
        ps.Stop();
        ps.Clear();
        ps.gameObject.SetActive(false);
        


        return ps;

        

    }



}

