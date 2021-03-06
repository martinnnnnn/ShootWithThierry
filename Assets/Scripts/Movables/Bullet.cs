﻿using UnityEngine;
using System.Collections.Generic;


public enum WEAPON_TYPE
{
    PISTOL,
    SNIPER,
    ROCKET,
    MONSTER,
    FRAGMENT,
}

public class Bullet : MonoBehaviour
{

    [HideInInspector]
    private WEAPON_TYPE BulletType;
    private int BulletDamage;
    private float RocketExplosionTime;

    //private Rigidbody2D myRigidBody;
    public SpriteRenderer myRenderer;

    private List<Vector2> rocketBulletsDirections;

    void Update()
    {
        if (BulletType == WEAPON_TYPE.ROCKET)
        {
            if (Time.timeSinceLevelLoad > RocketExplosionTime)
            {
                Explode();
                DeapthManager.Instance.RemoveActor(gameObject);
                gameObject.SetActive(false);
            }
        }
    }


    void Awake()
    {
        //myRigidBody = GetComponent<Rigidbody2D>();
        //myRenderer = GetComponent<SpriteRenderer>();

        rocketBulletsDirections = new List<Vector2>();
        rocketBulletsDirections.Add(new Vector2(0,1));
        rocketBulletsDirections.Add(new Vector2(0,-1));
        rocketBulletsDirections.Add(new Vector2(1,0));
        rocketBulletsDirections.Add(new Vector2(-1,0));
        rocketBulletsDirections.Add(new Vector2(.5f,.5f));
        rocketBulletsDirections.Add(new Vector2(-.5f,.5f));
        rocketBulletsDirections.Add(new Vector2(.5f,-.5f));
        rocketBulletsDirections.Add(new Vector2(-.5f,-.5f));
    }


    public void SetWeaponType(WEAPON_TYPE type)
    {
        BulletType = type;
        if (BulletType == WEAPON_TYPE.FRAGMENT)
        {
            myRenderer.sprite = Resources.Load<Sprite>("BULLET/IMG_BULLET_" + BulletType.ToString() + "_" + Random.Range(1,12));
        }
        else
        {
            myRenderer.sprite = Resources.Load<Sprite>("BULLET/IMG_BULLET_" + BulletType.ToString());
        }
        //gameObject.layer = LayerMask.NameToLayer("Bullet");
        switch (BulletType)
        {
            case WEAPON_TYPE.PISTOL:
                myRenderer.transform.localScale = new Vector3(2, 2, 1);
                BulletDamage = GameDataManager.Instance.PistolDamage;
                break;
            case WEAPON_TYPE.SNIPER:
                myRenderer.transform.localScale = new Vector3(1, 1, 1);
                BulletDamage = GameDataManager.Instance.SniperDamage;
                break;
            case WEAPON_TYPE.ROCKET:
                myRenderer.transform.localScale = new Vector3(2, 2, 1);
                BulletDamage = GameDataManager.Instance.RocketDamage;
                RocketExplosionTime = Time.timeSinceLevelLoad + GameDataManager.Instance.RocketTimeBeforeExplosion;
                break;
            case WEAPON_TYPE.MONSTER:
                myRenderer.transform.localScale = new Vector3(2, 2, 1);
                BulletDamage = GameDataManager.Instance.MonsterBulletDamage;
                break;
            case WEAPON_TYPE.FRAGMENT:
                myRenderer.transform.localScale = new Vector3(5, 5, 1);
                BulletDamage = GameDataManager.Instance.FragmentDamage;
                break;

        }
    }


    public void OnCollisionEnter2D(Collision2D c)
    {
        Monster monster = c.gameObject.GetComponent<Monster>();
        Enemy enemy = c.gameObject.GetComponent<Enemy>();
        Hero hero = c.gameObject.GetComponent<Hero>();

        if (c.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (BulletType == WEAPON_TYPE.ROCKET)
            {
                Explode();
                DeapthManager.Instance.RemoveActor(gameObject);
                gameObject.SetActive(false);
                return;
            }
            DeapthManager.Instance.RemoveActor(gameObject);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        
        switch (BulletType)
        {
            case WEAPON_TYPE.PISTOL:
            case WEAPON_TYPE.FRAGMENT:
                if (monster)
                {
                    monster.ChangeLife(BulletDamage);
                }
                if (enemy)
                {
                    enemy.ChangeLife(-BulletDamage);
                    if (enemy.GetEnemyType() == ENEMY_TYPE.GOURMAND)
                    {
                        enemy.ResetLastHitTimer();
                    }
                }
                DeapthManager.Instance.RemoveActor(gameObject);
                gameObject.SetActive(false);
                //Destroy(gameObject);
                break;

            case WEAPON_TYPE.SNIPER:
                if (monster)
                {
                    monster.ChangeLife(BulletDamage);
                    DeapthManager.Instance.RemoveActor(gameObject);
                    gameObject.SetActive(false);
                    //Destroy(gameObject);
                }
                if (enemy)
                {
                    
                    if (enemy.GetEnemyType() == ENEMY_TYPE.GOURMAND)
                    {
                        enemy.ResetLastHitTimer();
                    }
                    enemy.ChangeLife(-BulletDamage);
                    if (enemy.GetLife() > 0)
                    {
                        DeapthManager.Instance.RemoveActor(gameObject);
                        gameObject.SetActive(false);
                        //Destroy(gameObject);
                    }
                }
                break;
            case WEAPON_TYPE.ROCKET:
                Explode();
                DeapthManager.Instance.RemoveActor(gameObject);
                gameObject.SetActive(false);
                //Destroy(gameObject);
                break;
            case WEAPON_TYPE.MONSTER:
                if (hero)
                {
                    hero.ChangeLife(-BulletDamage);
                }
                if (enemy)
                {
                    enemy.ChangeLife(-BulletDamage);
                }
                DeapthManager.Instance.RemoveActor(gameObject);
                gameObject.SetActive(false);
                //Destroy(gameObject);
                break;
        }
        
    }


    
   

    public void Explode()
    {
        SoundManager.Instance.PlaySound("Weapon_Rocket_Explosion");
        foreach (Vector2 direction in rocketBulletsDirections)
        {
            BulletManager.Instance.FireBullet(WEAPON_TYPE.FRAGMENT, transform, direction);
        }
    }
}
