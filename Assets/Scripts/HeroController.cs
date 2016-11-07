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

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");
        myRigidBody.velocity = new Vector2(hMove * maxSpeed, vMove * maxSpeed);

        //transform.position = new Vector2(
        //    transform.position.x + hMove * maxSpeed * Time.fixedDeltaTime,
        //    transform.position.y + vMove * maxSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Input.GetKey("Fire1"))
        {

        }
    }


    void OnCollisionEnter2D(Collider2D c)
    {
        DroppedWeapon dropped = c.gameObject.GetComponent<DroppedWeapon>();
        if (dropped)
        {
            secondaryWeapon.bulletsLeft = dropped.numberOfAmmo;
            secondaryWeapon.SetType(dropped.weaponType);
        }
    }


}
