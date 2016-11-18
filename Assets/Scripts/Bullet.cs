using UnityEngine;
using System.Collections.Generic;



public class Bullet : MonoBehaviour
{


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
        Debug.Log("starting : " + bulletType.ToString());
    }
    


    public void OnCollisionEnter2D(Collision2D c)
    {
        MortalCharacter enemy = c.gameObject.GetComponent<MortalCharacter>();
        MonsterController monster = c.gameObject.GetComponent<MonsterController>();

        if (c.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
        
        switch (bulletType)
        {
            case WEAPON_TYPE.PISTOL:
                if (enemy)
                {
                    if (monster)
                    {
                        enemy.GainLife(damage);
                    }
                    else
                    {
                        enemy.LoseLife(damage);
                    }
                }
                Destroy(gameObject);
                break;
            case WEAPON_TYPE.SNIPER:
                if (enemy)
                {
                    if (monster)
                    {
                        enemy.GainLife(damage);
                        Destroy(gameObject);
                    }
                    else
                    {
                        enemy.LoseLife(damage);
                        if (enemy.life > 0)
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
