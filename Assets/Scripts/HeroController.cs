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

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        primaryWeapon = new Weapon();
        primaryWeapon.bulletsLeft = 40;

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
        if (Input.GetButton("Fire1") && primaryWeapon.bulletsLeft > 0)
        {
            --primaryWeapon.bulletsLeft;
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");
            Debug.Log("h:" + horizontal + " v:" + vertical);
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            //shootPoint.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            bulletManager.SpawnBullet(transform.position, Quaternion.Euler(new Vector3(0, angle, 0)), WEAPON_TYPE.BASIC);
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
