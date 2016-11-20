using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : Singleton<WavesManager>
{

    public Transform hero;
    public Transform monster;


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
            enemy.GetComponent<Enemy>().CurrentTarget = hero;
            enemy.SetActive(true);

            //GameObject enemy = Instantiate(commisPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
        }
        for (int i = 0; i < data.plongeurQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.PLONGEUR);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().CurrentTarget = hero;
            enemy.SetActive(true);
            //GameObject enemy = Instantiate(plongeurPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
        }
        for (int i = 0; i < data.gourmandQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.GOURMAND);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().CurrentTarget = hero;
            enemy.SetActive(true);
            //GameObject enemy = Instantiate(gourmandPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
        }
        for (int i = 0; i < data.gordonQuantity; ++i)
        {
            GameObject enemy = ObjectPool.Instance.GetPooledObject(POOLED_OBJECTS.GORDON);
            enemy.transform.position = spawningPlaces[data.spawningPosition].position;
            enemy.GetComponent<Enemy>().CurrentTarget = hero;
            enemy.SetActive(true);
            //GameObject enemy = Instantiate(gordonPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
        }
    }
    
}
