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
    public GameObject monsterPrefab;
    public float monsterSpeed;

    void Start ()
    {
        pistolAmmo = 400;
    }

    public void FireBullet(WEAPON_TYPE type, Transform shooterTransform, Vector2 direction, GameObject ignore = null)
    {
        IncreaseAmmo(type, -1);


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
            case WEAPON_TYPE.MONSTER:
                bulletSpeed = monsterSpeed;
                bulletType = monsterPrefab;
                break;
            default:
                bulletSpeed = pistolSpeed;
                bulletType = pistolPrefab;
                break;
        }
        GameObject bullet = Instantiate(bulletType, shooterTransform.position, shooterTransform.rotation) as GameObject;
        direction.Normalize();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        bullet.GetComponent<Bullet>().SetWeaponType(type);
        float deg = Vector2.Angle(new Vector2(1, 0), direction);

        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, deg);
        if (ignore) Physics2D.IgnoreCollision(ignore.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
    }
    


    public bool CanFirePistol(float fireDelay)
    {
        return (pistolAmmo > 0 && fireDelay >= pistolFireRate);
    }

    public bool CanFireGun(WEAPON_TYPE type, float fireDelay)
    {
        bool canfire = false;
        switch(type)
        {
            case WEAPON_TYPE.PISTOL:
                canfire = (pistolAmmo > 0 && fireDelay >= pistolFireRate);
                break;
            case WEAPON_TYPE.SNIPER:
                canfire = (sniperAmmo > 0 && fireDelay >= sniperFireRate);
                Debug.Log("ammo:" + sniperAmmo + " fireDelay: " + fireDelay + " / pistolFireRate: " + sniperFireRate);
                Debug.Log("canfire:" + canfire);
                break;
            case WEAPON_TYPE.ROCKET:
                canfire = (rocketAmmo > 0 && fireDelay >= rocketFireRate);
                break;
        }
        
        return canfire;
    }


    public void IncreaseAmmo(WEAPON_TYPE type, int amount)
    {
        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                pistolAmmo += amount;
                break;
            case WEAPON_TYPE.SNIPER:
                sniperAmmo += amount;
                break;
            case WEAPON_TYPE.ROCKET:
                rocketAmmo += amount;
                break;
        }
    }   
}
