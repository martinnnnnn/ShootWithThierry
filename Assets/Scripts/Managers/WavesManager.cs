using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : Singleton<WavesManager>
{
    private List<Transform> spawningPlaces;
    
    void Start()
    {
        spawningPlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            spawningPlaces.Add(t);
        }
    }


    public void SpawnWave(WaveData data)
    {
        for (int i = 0; i < data.commitQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.COMMIT);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.COMMIS);
            enemy.SetActive(true);

        }
        for (int i = 0; i < data.plongeurQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.PLONGEUR);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.PLONGEUR);
            enemy.SetActive(true);
        }
        for (int i = 0; i < data.gourmandQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.GOURMAND);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.GOURMAND);
            enemy.SetActive(true);
        }
        for (int i = 0; i < data.gordonQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.GORDON);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.GORDON);
            enemy.SetActive(true);
        }
    }
    
}


//GameObject enemy = Instantiate(gordonPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
