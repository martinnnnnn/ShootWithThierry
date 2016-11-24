using UnityEngine;
using System.Collections;




public class Hero : MonoBehaviour
{

    private float HeroSpeed;
    private Rigidbody2D HeroRigidBody;

    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.05f;

    [HideInInspector]
    private int HeroLife;

    private float currentFiringDelay;


    void Start ()
    {
        HeroLife = GameDataManager.Instance.HeroStartingLife;
        HeroSpeed = GameDataManager.Instance.HeroSpeed;
        HeroRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
        UIManager.Instance.SetHeroLife(HeroLife);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal1");
        float vMove = Input.GetAxis("Vertical1");
        if (canMove)
        {
            HeroRigidBody.velocity = new Vector2(hMove * HeroSpeed, vMove * HeroSpeed);
        }
    }

    void Update()
    {
        Fire();
        if (!canMove)
        {
            if (timeEnableMove == 0)
            {
                timeEnableMove = Time.timeSinceLevelLoad + timeUnmovable;
            }
            else
            {
                if (Time.timeSinceLevelLoad > timeEnableMove)
                {
                    canMove = true;
                    timeEnableMove = 0;
                }
            }
        }
    }


    public void OnCollisionEnter2D(Collision2D c)
    {
        Loot loot = c.gameObject.GetComponent<Loot>();
        if (loot)
        {
            int amount = loot.GetLootAmount();
            switch (loot.GetLootType())
            {
                case LOOT_TYPE.PISTOL:
                    BulletManager.Instance.ChangeAmmo(amount);
                    break;
                case LOOT_TYPE.SNIPER:
                    BulletManager.Instance.SetWeaponType(WEAPON_TYPE.SNIPER);
                    break;
                case LOOT_TYPE.ROCKET:
                    BulletManager.Instance.SetWeaponType(WEAPON_TYPE.ROCKET);
                    break;
                case LOOT_TYPE.LIFE:
                    if (HeroLife < GameDataManager.Instance.HeroStartingLife)
                    {
                        HeroLife ++;
                        
                    }
                    break;
            }
            Destroy(c.gameObject);
        }
    }


    void Fire()
    {
        currentFiringDelay += Time.deltaTime;
        
           // if (Input.GetButton("Fire1") && pistolAmmo > 0 && currentFiringDelay >= pistolFireRate)
        if (Input.GetButton("Fire1") && BulletManager.Instance.CanFireGun(WEAPON_TYPE.PISTOL,currentFiringDelay))
        {
            currentFiringDelay = 0f;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");

            if (horizontal != 0 || vertical != 0)
            {
                BulletManager.Instance.FireBullet(WEAPON_TYPE.PISTOL, transform, new Vector2(horizontal, vertical));
            }
        }

        if (Input.GetButton("Fire2"))
        {
            if (BulletManager.Instance.CanFireGun(WEAPON_TYPE.SNIPER, currentFiringDelay))
            {
                currentFiringDelay = 0f;
                float horizontal = Input.GetAxis("Horizontal2");
                float vertical = Input.GetAxis("Vertical2");

                if (horizontal != 0 || vertical != 0)
                {
                    Debug.Log("bullet tiré");
                    BulletManager.Instance.FireBullet(WEAPON_TYPE.SNIPER, transform, new Vector2(horizontal, vertical));
                }
            }
            else if (BulletManager.Instance.CanFireGun(WEAPON_TYPE.ROCKET, currentFiringDelay))
            {
                currentFiringDelay = 0f;
                float horizontal = Input.GetAxis("Horizontal2");
                float vertical = Input.GetAxis("Vertical2");

                if (horizontal != 0 || vertical != 0)
                {
                    BulletManager.Instance.FireBullet(WEAPON_TYPE.ROCKET, transform, new Vector2(horizontal, vertical));
                }
            }
        }
    }
    
    public void ChangeLife(int amount)
    {
        HeroLife += amount;
        UIManager.Instance.SetHeroLife(HeroLife);
    }



}
