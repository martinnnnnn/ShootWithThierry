using UnityEngine;
using System.Collections.Generic;


public enum ENEMY_TYPE
{
    BASIC,
    CLEANER,
    FAT,
    GORDON
}


public class Enemy : MonoBehaviour
{
        
    public Transform Target;
    public float MoveSpeed = 4;
    public float MinDist = 15;
    //  public float visionRadius = .1f;
    //  public LayerMask newTarget;

    public float attackRate = 1f;
    private float nextAttackTime;
    public int damage = 1;

    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.05f;


    void Start()
    {
        canMove = true;
    }

    void Awake()
    {
        canMove = true;
    }

    void FixedUpdate()
    {
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

        if (canMove) Deplacement();
        Attack();
    }

    void Deplacement()
    {

        Vector2 direction = Target.position - transform.position;
        float magnitude = direction.magnitude;
        direction.Normalize();
        
        Vector3 velocity = direction * MoveSpeed;

        if (magnitude >= MinDist)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * MoveSpeed, direction.y * MoveSpeed);
        }
        else if (magnitude < MinDist)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        

        transform.LookAt(Target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }

    void Attack()
    {
        if (Time.timeSinceLevelLoad > nextAttackTime)
        {
            nextAttackTime = Time.timeSinceLevelLoad + attackRate;

            if (Vector3.Distance(transform.position, Target.position) <= MinDist + 0.1f)
            {
                Target.GetComponent<MortalCharacter>().LoseLife(damage);
            }
        }
    }

}
