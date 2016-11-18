using UnityEngine;
using System.Collections;


public enum WEAPON_TYPE
{
    PISTOL,
    SNIPER,
    ROCKET,
}

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
    [HideInInspector]
    public int pistolAmmo;
    public float pistolFireRate;
    public GameObject pistolPrefab;
    public float pistolSpeed;
    [HideInInspector]
    public int sniperAmmo;
    public float sniperFireRate;
    public GameObject sniperPrefab;
    public float sniperSpeed;
    [HideInInspector]
    public int rocketAmmo;
    public float rocketFireRate;
    public GameObject rocketPrefab;
    public float rocketSpeed;

    void Start ()
    {
        life = 4;
        pistolAmmo = 400;
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
                    pistolAmmo += amount;
                    break;
                case LOOT_TYPE.SNIPER:
                    sniperAmmo += amount;
                    rocketAmmo = 0;
                    break;
                case LOOT_TYPE.ROCKET:
                    rocketAmmo += amount;
                    sniperAmmo = 0;
                    break;
                case LOOT_TYPE.LIFE:
                    if (life < 4)
                    {
                        life ++;
                    }
                    break;
            }
        }
    }


    void Fire(float deltaTime)
    {
        currentFiringDelay += deltaTime;
        if (Input.GetButton("Fire1") && pistolAmmo > 0 && currentFiringDelay >= pistolFireRate)
        {
            currentFiringDelay = 0f;
            --pistolAmmo;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");

            if (horizontal != 0 || vertical != 0)
            {
                BulletManager.Instance.FireBullet(WEAPON_TYPE.ROCKET, transform, new Vector2(horizontal, vertical));
            }
        }

        if (Input.GetButton("Fire2"))
        {
            if (sniperAmmo > 0 && currentFiringDelay >= sniperFireRate)
            {
                currentFiringDelay = 0f;
                --sniperAmmo;
                float horizontal = Input.GetAxis("Horizontal2");
                float vertical = Input.GetAxis("Vertical2");

                if (horizontal != 0 || vertical != 0)
                {
                    BulletManager.Instance.FireBullet(WEAPON_TYPE.SNIPER, transform, new Vector2(horizontal, vertical));
                }
            }
        }
    }

    //private void FireBullet(WEAPON_TYPE type, Vector2 direction)
    //{
    //    float bulletSpeed = 0f;
    //    GameObject bulletType;
    //    switch(type)
    //    {
    //        case WEAPON_TYPE.PISTOL:
    //            bulletSpeed = pistolSpeed;
    //            bulletType = pistolPrefab;
    //            break;
    //        case WEAPON_TYPE.SNIPER:
    //            bulletSpeed = sniperSpeed;
    //            bulletType = sniperPrefab;
    //            break;
    //        case WEAPON_TYPE.ROCKET:
    //            bulletSpeed = rocketSpeed;
    //            bulletType = rocketPrefab;
    //            break;
    //        default:
    //            bulletSpeed = pistolSpeed;
    //            bulletType = pistolPrefab;
    //            break;
    //    }
    //    GameObject bullet = Instantiate(bulletType, transform.position, transform.rotation) as GameObject;
    //    direction.Normalize();
    //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * pistolSpeed, direction.y * pistolSpeed);

    //    float deg = Vector2.Angle(new Vector2(1, 0), direction);

    //    bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, deg);
    //}

}
