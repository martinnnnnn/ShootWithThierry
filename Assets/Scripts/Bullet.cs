using UnityEngine;
using System.Collections.Generic;


public enum WEAPON_TYPE
{
    PISTOL,
    SNIPER,
    ROCKET,
    MONSTER,
}

public class Bullet : MonoBehaviour
{

    [HideInInspector]
    public WEAPON_TYPE bulletType;
    public int damage = 1;
    public float timeBeforeExplosion = 1f;
    private float explosionTime;

    private Rigidbody2D myRigidBody;
    private SpriteRenderer myRenderer;

    private List<Vector2> rocketBulletsDirections;

    void Update()
    {
        if (bulletType == WEAPON_TYPE.ROCKET)
        {
            if (Time.timeSinceLevelLoad > explosionTime)
            {
                Explode();
                
            }
        }
    }


    void Awake()
    {
        explosionTime = Time.timeSinceLevelLoad + timeBeforeExplosion;
        rocketBulletsDirections = new List<Vector2>();
        rocketBulletsDirections.Add(new Vector2(0,1));
        rocketBulletsDirections.Add(new Vector2(0,-1));
        rocketBulletsDirections.Add(new Vector2(1,0));
        rocketBulletsDirections.Add(new Vector2(-1,0));
        rocketBulletsDirections.Add(new Vector2(.5f,.5f));
        rocketBulletsDirections.Add(new Vector2(-.5f,.5f));
        rocketBulletsDirections.Add(new Vector2(.5f,-.5f));
        rocketBulletsDirections.Add(new Vector2(-.5f,-.5f));

        myRigidBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sprite = Resources.Load<Sprite>("IMG_" + bulletType.ToString());
    }
    


    public void OnCollisionEnter2D(Collision2D c)
    {
        MortalCharacter mortal = c.gameObject.GetComponent<MortalCharacter>();
        MonsterController monster = c.gameObject.GetComponent<MonsterController>();
        Enemy enemy = c.gameObject.GetComponent<Enemy>();

        if (c.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
        
        switch (bulletType)
        {
            case WEAPON_TYPE.PISTOL:
                if (mortal)
                {
                    if (monster)
                    {
                        mortal.GainLife(damage);
                    }
                    else if (enemy)
                    {
                        if (enemy.type == ENEMY_TYPE.FAT)
                        {
                            enemy.ResetLastHitTimer();
                        }
                        mortal.LoseLife(damage);
                    }
                }
                Destroy(gameObject);
                break;
            case WEAPON_TYPE.SNIPER:
                if (mortal)
                {
                    if (monster)
                    {
                        mortal.GainLife(damage);
                        Destroy(gameObject);
                    }
                    else if (enemy)
                    {
                        if (enemy.type == ENEMY_TYPE.FAT)
                        {
                            enemy.ResetLastHitTimer();
                        }
                        mortal.LoseLife(damage);
                        if (mortal.life > 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                break;
            case WEAPON_TYPE.ROCKET:
                Explode();
                break;
        }
        
    }


    public void SetWeaponType(WEAPON_TYPE type)
    {
        bulletType = type;
        myRenderer.sprite = Resources.Load<Sprite>("IMG_" + bulletType.ToString());
    }
   

    public void Explode()
    {
        foreach (Vector2 direction in rocketBulletsDirections)
        {
            BulletManager.Instance.FireBullet(WEAPON_TYPE.PISTOL, transform, direction);
        }
        Destroy(gameObject);
    }
}
