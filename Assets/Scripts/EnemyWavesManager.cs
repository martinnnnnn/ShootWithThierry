using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWavesManager : MonoBehaviour
{


    public GameObject commisPrefab;
    public GameObject plongeurPrefab;
    public GameObject gourmandPrefab;
    public GameObject gordonPrefab;


    private List<Transform> spawningPlaces;


    private int pos = 0;

    public List<ENEMY_TYPE> typeDenemis;

    void Start()
    {
        spawningPlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            spawningPlaces.Add(t);
        }
    }


    void Update()
    {



   
    }


    public void SpawnWave(ENEMY_TYPE type, int numberOfEnemy, Vector3 position)
    {
        for (int i = 0; i < numberOfEnemy; ++i)
        {
            //GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
            //enemy.transform.position = position;
        }
        
    }
}
