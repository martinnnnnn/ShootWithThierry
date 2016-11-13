using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{




    public float maxSpeed = 5f;
    private Rigidbody2D myRigidBody;

    [HideInInspector]
    public Weapon primaryWeapon;

    [HideInInspector]
    public Weapon secondaryWeapon;

    public BulletManager bulletManager;
    public GameObject bullet;
    private float bulletSpeed = 10f;
    public float fireRate = 0.1f;
    private float currentFiringDelay;
    // Use this for initialization


    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        primaryWeapon = new Weapon();
        primaryWeapon.bulletsLeft = 20000;
        currentFiringDelay = fireRate;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal1");
        float vMove = Input.GetAxis("Vertical1");
        myRigidBody.velocity = new Vector2(hMove * maxSpeed, vMove * maxSpeed);

        //transform.position = new Vector2(
        //    transform.position.x + hMove * maxSpeed * Time.fixedDeltaTime,
        //    transform.position.y + vMove * maxSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        currentFiringDelay += Time.deltaTime;
        if (Input.GetButton("Fire1") && primaryWeapon.bulletsLeft > 0 && currentFiringDelay >= fireRate)
        {
            currentFiringDelay = 0f;
            --primaryWeapon.bulletsLeft;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");
            if (horizontal != 0 || vertical != 0)
            {
                GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * bulletSpeed, vertical * bulletSpeed);

                float deg = Vector2.Angle(new Vector2(1, 0), new Vector2(horizontal, vertical));
                if (vertical < 0)
                {
                    //    deg = 360 - deg;
                }

                go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, deg);
                Physics2D.IgnoreCollision(go.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            //float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            //shootPoint.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            //bulletManager.SpawnBullet(transform.position, Quaternion.Euler(new Vector3(0, angle, 0)), WEAPON_TYPE.BASIC);
            //bulletManager.SpawnBullet(transform.position, transform.rotation, new Vector2(horizontal,vertical), WEAPON_TYPE.BASIC);
        }
    }


    //void OnCollisionEnter2D(Collider2D c)
    //{
    //    DroppedWeapon dropped = c.gameObject.GetComponent<DroppedWeapon>();
    //    if (dropped)
    //    {
    //        secondaryWeapon.bulletsLeft = dropped.numberOfAmmo;
    //        secondaryWeapon.SetType(dropped.weaponType);
    //    }
    //}


}
