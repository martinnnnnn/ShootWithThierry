using UnityEngine;
using System.Collections;




public class DashController : MonoBehaviour
{
    private HeroController hero;
    private Enemy enemy;
    private Rigidbody2D myRigidBody;
    public LayerMask bullet;
    public float visionRadius = 0.5f;
    public float dashSpeed = 10f;
    private float timeNextDash;
    public float timeBetweenDashes = 2f;

    void Start()
    {
        hero = GetComponent<HeroController>();
        enemy = GetComponent<Enemy>();
        myRigidBody = GetComponent<Rigidbody2D>();
        timeNextDash = 0f;
    }

    void FixedUpdate()
    {
        if (hero)
        {
            if (Input.GetButton("Jump") && Time.timeSinceLevelLoad >= timeNextDash)
            {
                timeNextDash = Time.timeSinceLevelLoad + timeBetweenDashes;
                Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
                myRigidBody.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);
                hero.canMove = false;
            }
        }
        else
        {
            Collider2D incomingBullet = Physics2D.OverlapCircle(transform.position, visionRadius, bullet);
            if (incomingBullet)
            {
                Vector2 dashDirection = transform.position - incomingBullet.transform.position;
                dashDirection.Normalize();
                myRigidBody.velocity = new Vector2(-dashDirection.y * dashSpeed, dashDirection.x * dashSpeed);
                enemy.canMove = false;
            }
        }
    }
}
