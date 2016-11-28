using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class BulletManager : Singleton<BulletManager>
{
    private int Ammo;

    private int pistolAmmoPerShot;
    private int sniperAmmoPerShot;
    private int rocketAmmoPerShot;

    private float pistolFireRate;
    private float sniperFireRate;
    private float rocketFireRate;

    private float pistolSpeed;
    private float sniperSpeed;
    private float rocketSpeed;
    private float monsterSpeed;
    private float fragmentSpeed;

    private WEAPON_TYPE currentWeapon;

    void Start ()
    {
        Ammo = GameDataManager.Instance.StartingAmmo;

        pistolAmmoPerShot = GameDataManager.Instance.PistolAmmoPerShot;
        sniperAmmoPerShot = GameDataManager.Instance.SniperAmmoPerShot;
        rocketAmmoPerShot = GameDataManager.Instance.RocketAmmoPerShot;

        pistolFireRate = GameDataManager.Instance.PistolFireRate;
        sniperFireRate = GameDataManager.Instance.SniperFireRate;
        rocketFireRate = GameDataManager.Instance.RocketFireRate;

        pistolSpeed = GameDataManager.Instance.PistolSpeed;
        sniperSpeed = GameDataManager.Instance.SniperSpeed;
        rocketSpeed = GameDataManager.Instance.RocketSpeed;
        monsterSpeed = GameDataManager.Instance.MonsterBulletSpeed;
        fragmentSpeed = GameDataManager.Instance.FragmentSpeed;
        currentWeapon = WEAPON_TYPE.PISTOL;

        UIManager.Instance.SetAmmo(Ammo);
    }

    public void FireBullet(WEAPON_TYPE type, Transform shooterTransform, Vector2 direction, GameObject ignore = null, GameObject unIgnore = null)
    {
        float bulletSpeed = 0f;
        direction.Normalize();

        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                ChangeAmmo(-pistolAmmoPerShot);
                bulletSpeed = pistolSpeed;
                break;
            case WEAPON_TYPE.SNIPER:
                ChangeAmmo(-sniperAmmoPerShot);
                bulletSpeed = sniperSpeed;
                break;
            case WEAPON_TYPE.ROCKET:
                ChangeAmmo(-rocketAmmoPerShot);
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

        GameObject bullet = ObjectPool.GetNextObject(GameDataManager.Instance.Bullet, shooterTransform);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        bullet.GetComponent<Bullet>().SetWeaponType(type);

        if (ignore) Physics2D.IgnoreCollision(ignore.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
    }

    public bool CanFireGun(WEAPON_TYPE type, float fireDelay)
    {
        bool canfire = false;
        switch(type)
        {
            case WEAPON_TYPE.PISTOL:
                canfire = (Ammo > pistolAmmoPerShot && fireDelay >= pistolFireRate);
                break;
            case WEAPON_TYPE.SNIPER:
                canfire = (Ammo > sniperAmmoPerShot && currentWeapon == WEAPON_TYPE.SNIPER && fireDelay >= sniperFireRate);
                break;
            case WEAPON_TYPE.ROCKET:
                canfire = (Ammo > rocketAmmoPerShot && currentWeapon == WEAPON_TYPE.ROCKET && fireDelay >= rocketFireRate);
                break;
        }
        
        return canfire;
    }


    public void ChangeAmmo(int amount)
    {
        if (Ammo + amount >= 0)
        {
            Ammo += amount;
        }
        UIManager.Instance.SetAmmo(Ammo);
    }

    public void SetAmmo(int value)
    {
        if (value > 0)
        {
            Ammo = value;
        }
        UIManager.Instance.SetAmmo(Ammo);
    }

    public void SetWeaponType(WEAPON_TYPE type)
    {
        currentWeapon = type;
    }
}
