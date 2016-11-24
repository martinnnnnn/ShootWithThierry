using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : Singleton<WavesManager>
{
    private List<Transform> spawningPlaces;

    private GameObject commisPrefab;
    private GameObject plongeurPrefab;
    private GameObject gourmandPrefab;
    private GameObject gordonPrefab;


    void Start()
    {

        commisPrefab = GameDataManager.Instance.Commis;
        plongeurPrefab = GameDataManager.Instance.Plongeur;
        gourmandPrefab = GameDataManager.Instance.Gourmand;
        gordonPrefab = GameDataManager.Instance.Gordon;

        spawningPlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            spawningPlaces.Add(t);
        }


    }


    public IEnumerator SpawnWave(WaveData data)
    {
        WaitForSeconds wait = new WaitForSeconds(0.001f);
        for (int i = 0; i < data.commitQuantity; ++i)
        {
            GameObject enemy = ObjectPool.GetNextObject(commisPrefab, spawningPlaces[data.spawningPosition]);
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.COMMIS);
            enemy.SetActive(true);
            yield return wait;
        }
        for (int i = 0; i < data.plongeurQuantity; ++i)
        {
            GameObject enemy = ObjectPool.GetNextObject(plongeurPrefab, spawningPlaces[data.spawningPosition]);
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.PLONGEUR);
            enemy.SetActive(true);
            yield return wait;
        }
        for (int i = 0; i < data.gourmandQuantity; ++i)
        {
            GameObject enemy = ObjectPool.GetNextObject(gourmandPrefab, spawningPlaces[data.spawningPosition]);
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.GOURMAND);
            enemy.SetActive(true);
            yield return wait;
        }
        for (int i = 0; i < data.gordonQuantity; ++i)
        {
            GameObject enemy = ObjectPool.GetNextObject(gordonPrefab, spawningPlaces[data.spawningPosition]);
            enemy.GetComponent<Enemy>().SetType(ENEMY_TYPE.GORDON);
            enemy.SetActive(true);
            yield return wait;
        }
    }


    
}


//GameObject enemy = Instantiate(gordonPrefab, spawningPlaces[data.spawningPosition].position, new Quaternion()) as GameObject;
