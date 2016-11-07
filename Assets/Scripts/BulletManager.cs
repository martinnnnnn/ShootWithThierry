﻿using UnityEngine;
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


    public void SpawnBullet(Vector3 position, Quaternion rotation, WEAPON_TYPE type)
    {
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Bullet>().weaponType = type;
        bullet.SetActive(true);
    }
    
    
}
