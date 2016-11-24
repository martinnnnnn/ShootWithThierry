using UnityEngine;
using System.Collections;




public class DashController : MonoBehaviour
{
    private Hero hero;
    private Enemy enemy;
    private Rigidbody2D myRigidBody;
    private LayerMask bullet;
    private float visionRadius = 0.5f;

    private float HeroDashSpeed;
    private float HeroDashCoolDown;
    private float EnemyDashSpeed;
    private float EnemyDashCoolDown;
    private float timeNextDash;

    void Start()
    {
        hero = GetComponent<Hero>();
        enemy = GetComponent<Enemy>();
        myRigidBody = GetComponent<Rigidbody2D>();
        bullet = LayerMask.NameToLayer("Bullet");
        HeroDashSpeed = GameDataManager.Instance.HeroDashSpeed;
        HeroDashCoolDown = GameDataManager.Instance.HeroDashCoolDown;
        EnemyDashSpeed = GameDataManager.Instance.PlongeurDashSpeed;
        EnemyDashCoolDown = GameDataManager.Instance.PlongeurDashCoolDown;

        timeNextDash = 0f;
    }

    void FixedUpdate()
    {
        if (hero)
        {
            if (Input.GetButton("Jump") && Time.timeSinceLevelLoad >= timeNextDash)
            {
                Debug.Log("Jump!");
                timeNextDash = Time.timeSinceLevelLoad + HeroDashCoolDown;
                Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal1"),Input.GetAxis("Vertical1"));
                myRigidBody.velocity = new Vector2(dashDirection.x * HeroDashSpeed, dashDirection.y * HeroDashSpeed);
                hero.canMove = false;
            }
        }
        else
        {
            Collider2D incomingBullet = Physics2D.OverlapCircle(transform.position, visionRadius, bullet);
            if (incomingBullet && Time.timeSinceLevelLoad >= timeNextDash)
            {
                timeNextDash = Time.timeSinceLevelLoad + EnemyDashCoolDown;
                Vector2 dashDirection = transform.position - incomingBullet.transform.position;
                dashDirection.Normalize();
                myRigidBody.velocity = new Vector2(-dashDirection.y * EnemyDashSpeed, dashDirection.x * EnemyDashSpeed);
                enemy.canMove = false;
            }
        }
    }
}
