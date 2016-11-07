using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWavesManager : MonoBehaviour
{

    ObjectPool enemyPool;
    public GameObject enemyPrefab;
    public int initialSize;

    public Transform spawningPosition1;
    public Transform spawningPosition2;
    public Transform spawningPosition3;
    public Transform spawningPosition4;

    public Transform target;

    private int pos = 0;

    void Start()
    {
        enemyPool = new ObjectPool();
        enemyPool.InitPool(enemyPrefab, initialSize);
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(pos)
            {
                case 0:
                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1,10), spawningPosition1.position);
                    break;
                case 1:
                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPosition2.position);
                    break;
                case 2:
                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPosition3.position);
                    break;
                case 3:
                    SpawnWave(ENEMY_TYPE.BASIC, Random.Range(1, 10), spawningPosition4.position);
                    break;
            }
            pos = pos + 1 % 4;
        }
    }


    public void SpawnWave(ENEMY_TYPE type, int numberOfEnemy, Vector3 position)
    {
        for (int i = 0; i < numberOfEnemy; ++i)
        {
            GameObject enemy = enemyPool.GetPooledObject();
            enemy.transform.position = position;
            enemy.GetComponent<Enemy>().Target = target;
            enemy.SetActive(true);
        }
        
    }
}
