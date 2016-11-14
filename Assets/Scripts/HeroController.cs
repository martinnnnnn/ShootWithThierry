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
        FiringController fire = GetComponent<FiringController>();
        fire.InitFiring(FIRING_TYPE.BASIC, fireRate, 4000, bullet, bulletSpeed);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal1");
        float vMove = Input.GetAxis("Vertical1");
        myRigidBody.velocity = new Vector2(hMove * maxSpeed, vMove * maxSpeed);
        
    }

    void Update()
    {

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
