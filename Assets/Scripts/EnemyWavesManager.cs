using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyWavesManager : MonoBehaviour
{

    ObjectPool enemyPool;
    public GameObject enemyPrefab;
    public int initialSize;

    void Start()
    {
        enemyPool = new ObjectPool();
        enemyPool.InitPool(enemyPrefab, initialSize);
    }


    public void SpawnBullet(Vector3 position, Quaternion rotation, WEAPON_TYPE type)
    {
        GameObject bullet = enemyPool.GetPooledObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Bullet>().weaponType = type;
        bullet.SetActive(true);
    }
    
    
}
