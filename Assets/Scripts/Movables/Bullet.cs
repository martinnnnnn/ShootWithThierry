using UnityEngine;
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

    private Rigidbody2D myRigidBody;
    private SpriteRenderer myRenderer;

    private List<Vector2> rocketBulletsDirections;

    void Update()
    {
        if (BulletType == WEAPON_TYPE.ROCKET)
        {
            if (Time.timeSinceLevelLoad > RocketExplosionTime)
            {
                Explode();
            }
        }
    }


    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();

        RocketExplosionTime = Time.timeSinceLevelLoad + GameManager.Instance.RocketTimeBeforeExplosion;
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
        myRenderer.sprite = Resources.Load<Sprite>("BULLET/IMG_BULLET_" + BulletType.ToString());
        switch (BulletType)
        {
            case WEAPON_TYPE.PISTOL:
                BulletDamage = GameManager.Instance.PistolDamage;
                break;
            case WEAPON_TYPE.SNIPER:
                BulletDamage = GameManager.Instance.SniperDamage;
                break;
            case WEAPON_TYPE.ROCKET:
                BulletDamage = GameManager.Instance.RocketDamage;
                break;
            case WEAPON_TYPE.MONSTER:
                BulletDamage = GameManager.Instance.MonsterBulletDamage;
                break;
            case WEAPON_TYPE.FRAGMENT:
                BulletDamage = GameManager.Instance.FragmentDamage;
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
            Destroy(gameObject);
        }
        
        switch (BulletType)
        {
            case WEAPON_TYPE.PISTOL:
                if (monster)
                {
                    monster.ChangeLife(BulletDamage); ;
                }
                if (enemy)
                {
                    enemy.ChangeLife(-BulletDamage);
                    if (enemy.GetEnemyType() == ENEMY_TYPE.GOURMAND)
                    {
                        enemy.ResetLastHitTimer();
                    }
                }
                Destroy(gameObject);
                break;

            case WEAPON_TYPE.SNIPER:
                if (monster)
                {
                    monster.ChangeLife(BulletDamage);
                    Destroy(gameObject);
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
                        Destroy(gameObject);
                    }
                }
                break;
            case WEAPON_TYPE.ROCKET:
                if (monster)
                {
                    monster.ChangeLife(BulletDamage); ;
                }
                if (enemy)
                {
                    enemy.ChangeLife(-BulletDamage);
                    if (enemy.GetEnemyType() == ENEMY_TYPE.GOURMAND)
                    {
                        enemy.ResetLastHitTimer();
                    }
                }
                Explode();
                Destroy(gameObject);
                break;
            case WEAPON_TYPE.MONSTER:
                if (hero)
                {
                    hero.ChangeLife(-BulletDamage);
                }
                if (enemy)
                {
                    hero.ChangeLife(-BulletDamage);
                }
                Destroy(gameObject);
                break;
        }
        
    }


    
   

    public void Explode()
    {
        foreach (Vector2 direction in rocketBulletsDirections)
        {
            BulletManager.Instance.FireBullet(WEAPON_TYPE.FRAGMENT, transform, direction);
        }
    }
}
