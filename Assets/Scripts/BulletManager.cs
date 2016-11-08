using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BulletManager : MonoBehaviour
{

    ObjectPool bulletPool;
    public GameObject bulletPrefab;
    public int initialSize;
    public float bulletSpeed = 10f;

    void Start()
    {
        bulletPool = new ObjectPool();
        bulletPool.InitPool(bulletPrefab,initialSize);
    }


    public void SpawnBullet(Vector3 position, Quaternion rotation, Vector2 direction, WEAPON_TYPE type)
    {
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        bullet.GetComponent<Bullet>().weaponType = type;

        float deg = Vector2.Angle(new Vector2(1, 0), direction);
        if (direction.y < 0)
        {
            deg = 360 - deg;
        }
        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, deg);
        bullet.SetActive(true);
    }
    
    
}
