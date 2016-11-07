using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BulletManager : MonoBehaviour
{

    ObjectPool bulletPool;
    public GameObject bulletPrefab;
    public int initialSize;

    void Start()
    {
        bulletPool = new ObjectPool();
        bulletPool.InitPool(bulletPrefab,initialSize);
    }


    void SpawnBullet(Vector3 position, Vector3 rotation, WEAPON_TYPE type)
    {
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.SetActive(true);
    }
    
    
}
