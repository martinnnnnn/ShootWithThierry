using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class BulletManager : Singleton<BulletManager>
{


    public int pistolAmmo;
    public float pistolFireRate;
    public GameObject pistolPrefab;
    public float pistolSpeed;
    public int sniperAmmo;
    public float sniperFireRate;
    public GameObject sniperPrefab;
    public float sniperSpeed;
    public int rocketAmmo;
    public float rocketFireRate;
    public GameObject rocketPrefab;
    public float rocketSpeed;

    public void FireBullet(WEAPON_TYPE type, Transform shooterTransform, Vector2 direction)
    {
        float bulletSpeed = 0f;
        GameObject bulletType;
        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                bulletSpeed = pistolSpeed;
                bulletType = pistolPrefab;
                break;
            case WEAPON_TYPE.SNIPER:
                bulletSpeed = sniperSpeed;
                bulletType = sniperPrefab;
                break;
            case WEAPON_TYPE.ROCKET:
                bulletSpeed = rocketSpeed;
                bulletType = rocketPrefab;
                break;
            default:
                bulletSpeed = pistolSpeed;
                bulletType = pistolPrefab;
                break;
        }
        GameObject bullet = Instantiate(bulletType, shooterTransform.position, shooterTransform.rotation) as GameObject;
        direction.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * pistolSpeed, direction.y * pistolSpeed);
        bullet.GetComponent<Bullet>().SetWeaponType(type);
        float deg = Vector2.Angle(new Vector2(1, 0), direction);

        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, deg);
    }


    //ObjectPool bulletPool;
    //public GameObject bulletPrefab;
    //public int initialSize;
    //public float bulletSpeed = 10f;

    //void Start()
    //{
    //    bulletPool = new ObjectPool();
    //    bulletPool.InitPool(bulletPrefab,initialSize);
    //}


    //public void SpawnBullet(Vector3 position, Quaternion rotation, Vector2 direction, WEAPON_TYPE type)
    //{
    //    GameObject bullet = bulletPool.GetPooledObject();
    //    bullet.transform.position = position;
    //    bullet.transform.rotation = rotation;
    //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
    //    bullet.GetComponent<Bullet>().bulletType = type;

    //    float deg = Vector2.Angle(new Vector2(1, 0), direction);
    //    if (direction.y < 0)
    //    {
    //        deg = 360 - deg;
    //    }
    //    bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, deg);
    //    bullet.SetActive(true);
    //}


}
