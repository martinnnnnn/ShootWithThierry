using UnityEngine;
using System.Collections;




public class HeroController : MonoBehaviour
{

    public float maxSpeed = 5f;
    private Rigidbody2D myRigidBody;

    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.05f;

    [HideInInspector]
    public int life;

    private float currentFiringDelay;
    //[HideInInspector]
    //public int pistolAmmo;
    //public float pistolFireRate;
    //public GameObject pistolPrefab;
    //public float pistolSpeed;
    //[HideInInspector]
    //public int sniperAmmo;
    //public float sniperFireRate;
    //public GameObject sniperPrefab;
    //public float sniperSpeed;
    //[HideInInspector]
    //public int rocketAmmo;
    //public float rocketFireRate;
    //public GameObject rocketPrefab;
    //public float rocketSpeed;

    void Start ()
    {
        life = 3;
        myRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal1");
        float vMove = Input.GetAxis("Vertical1");
        if (canMove)
        {
            myRigidBody.velocity = new Vector2(hMove * maxSpeed, vMove * maxSpeed);
        }
    }

    void Update()
    {
        Fire(Time.deltaTime);
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
                    BulletManager.Instance.IncreaseAmmo(WEAPON_TYPE.PISTOL,amount);
                    break;
                case LOOT_TYPE.SNIPER:
                    BulletManager.Instance.IncreaseAmmo(WEAPON_TYPE.SNIPER, amount);
                    BulletManager.Instance.rocketAmmo = 0;
                    Debug.Log("sniper ammo:" + BulletManager.Instance.sniperAmmo);
                    break;
                case LOOT_TYPE.ROCKET:
                    BulletManager.Instance.IncreaseAmmo(WEAPON_TYPE.ROCKET, amount);
                    BulletManager.Instance.sniperAmmo = 0;
                    break;
                case LOOT_TYPE.LIFE:
                    if (life < 4)
                    {
                        life ++;
                        LootManager.Instance.canSpawnLife = true;
                    }
                    break;
            }
            Destroy(c.gameObject);
        }
    }


    void Fire(float deltaTime)
    {
        currentFiringDelay += deltaTime;
        
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
    

}
