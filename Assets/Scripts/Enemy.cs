using UnityEngine;
using System.Collections.Generic;


public enum ENEMY_TYPE
{
    COMMIS,
    CLEANER,
    FAT,
    GORDON
}


public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform CurrentTarget;
    public Transform Monster;
    public Transform Hero;

    private MortalCharacter myLife;

    public float DistanceChangementFocus = 2f;

    public float MoveSpeed = 4;
    public float MinDist = 15;

    public float attackRate = 1f;
    private float nextAttackTime;
    public int damage = 1;

    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.05f;

    public float TimeResetFocus = 5f;
    private float timeLastHit;

    public ENEMY_TYPE type;


    void Awake()
    {
        myLife = GetComponent<MortalCharacter>();
    }

    void OnEnable()
    {
        canMove = true;
        ResetCurrentTarget();
        myLife.life = 5;
    }

    void FixedUpdate()
    {
        HandleFocus();
        if (type == ENEMY_TYPE.CLEANER)
        {
            HandleDash();
        }
        if (canMove) Deplacement();
        Attack();
    }

    void Deplacement()
    {

        Vector2 direction = CurrentTarget.position - transform.position;
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

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.LookAt(CurrentTarget.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }

    void Attack()
    {
        if (Time.timeSinceLevelLoad > nextAttackTime)
        {
            nextAttackTime = Time.timeSinceLevelLoad + attackRate;

            if (Vector3.Distance(transform.position, CurrentTarget.position) <= MinDist + 0.1f)
            {
                CurrentTarget.GetComponent<MortalCharacter>().LoseLife(damage);
            }
        }
    }


    void HandleFocus()
    {
        switch(type)
        {
            case ENEMY_TYPE.COMMIS:
            case ENEMY_TYPE.CLEANER:
                if (CurrentTarget == Monster)
                {
                    if (Vector2.Distance(transform.position, Hero.position) < DistanceChangementFocus)
                    {
                        CurrentTarget = Hero;
                    }
                }
                else
                {

                }
                break;
            case ENEMY_TYPE.FAT:
                if (CurrentTarget == Monster && Time.timeSinceLevelLoad < timeLastHit + TimeResetFocus)
                {
                    CurrentTarget = Hero;
                }
                else if (CurrentTarget == Hero && Time.timeSinceLevelLoad > timeLastHit + TimeResetFocus)
                {
                    CurrentTarget = Monster;
                }
                break;
            case ENEMY_TYPE.GORDON:
                break;
        }
    }


    public void DropLoot()
    {
        if (type == ENEMY_TYPE.GORDON)
        {
            LootManager.Instance.SpawnRandomLoot(transform,1);
        }
    }


    void HandleDash()
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
    }

    public void ResetLastHitTimer()
    {
        timeLastHit = Time.timeSinceLevelLoad;
    }

    private void ResetCurrentTarget()
    {
        switch(type)
        {
            case ENEMY_TYPE.COMMIS:
            case ENEMY_TYPE.CLEANER:
            case ENEMY_TYPE.FAT:
                CurrentTarget = Monster;
                break;
            case ENEMY_TYPE.GORDON:
                CurrentTarget = Hero;
                break;
        }
    }
}
