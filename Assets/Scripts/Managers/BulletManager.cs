using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class BulletManager : Singleton<BulletManager>
{
    private int pistolAmmo;
    private int sniperAmmo;
    private int rocketAmmo;

    private float pistolFireRate;
    private float sniperFireRate;
    private float rocketFireRate;

    private float pistolSpeed;
    private float sniperSpeed;
    private float rocketSpeed;
    private float monsterSpeed;
    private float fragmentSpeed;

    void Start ()
    {
        pistolAmmo = GameManager.Instance.PistolStartingAmmo;
        sniperAmmo = 0;
        rocketAmmo = 0;

        pistolFireRate = GameManager.Instance.PistolFireRate;
        sniperFireRate = GameManager.Instance.SniperFireRate;
        rocketFireRate = GameManager.Instance.RocketFireRate;

        pistolSpeed = GameManager.Instance.PistolSpeed;
        sniperSpeed = GameManager.Instance.SniperSpeed;
        rocketSpeed = GameManager.Instance.RocketSpeed;
        monsterSpeed = GameManager.Instance.MonsterBulletSpeed;
    }

    public void FireBullet(WEAPON_TYPE type, Transform shooterTransform, Vector2 direction, GameObject ignore = null)
    {
        ChangeAmmo(type, -1);

        float bulletSpeed = 0f;
        
        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                bulletSpeed = pistolSpeed;
                break;
            case WEAPON_TYPE.SNIPER:
                bulletSpeed = sniperSpeed;
                break;
            case WEAPON_TYPE.ROCKET:
                bulletSpeed = rocketSpeed;
                break;
            case WEAPON_TYPE.MONSTER:
                bulletSpeed = monsterSpeed;
                break;
            case WEAPON_TYPE.FRAGMENT:
                bulletSpeed = fragmentSpeed;
                break;
            default:
                break;
        }
        GameObject bullet = Instantiate(GameManager.Instance.Bullet, shooterTransform.position, shooterTransform.rotation) as GameObject;
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


    public void ChangeAmmo(WEAPON_TYPE type, int amount)
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
            default:
                break;
        }
    }

    public void SetAmmo(WEAPON_TYPE type, int value)
    {
        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                pistolAmmo = value;
                break;
            case WEAPON_TYPE.SNIPER:
                sniperAmmo = value;
                break;
            case WEAPON_TYPE.ROCKET:
                rocketAmmo = value;
                break;
            default:
                break;
        }
    }
}
