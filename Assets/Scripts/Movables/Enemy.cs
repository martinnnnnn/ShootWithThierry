using UnityEngine;
using System.Collections.Generic;


public enum ENEMY_TYPE
{
    COMMIS,
    PLONGEUR,
    GOURMAND,
    GORDON
}


public class Enemy : MonoBehaviour
{

    [HideInInspector]
    private Transform CurrentTarget;
    private Transform Monster;
    private Transform Hero;

    private ENEMY_TYPE type;
    private int EnemyLife;
    private float DistanceChangementFocus;
    private float MoveSpeed;
    private float AttackSpeed;
    private int Damage;
    private float AttackDistanceHero;
    private float AttackDistanceMonster;


    [HideInInspector]
    public bool canMove;
    private float timeEnableMove;
    private float timeUnmovable = 0.05f;
    private float nextAttackTime;

    private float GourmandTimeResetFocus;
    private float timeLastHit;



    void Awake()
    {
        Monster = GameManager.Instance.Monster;
        Hero = GameManager.Instance.Hero;
        GourmandTimeResetFocus = GameManager.Instance.GourmandTimeResetFocus;
    }

    void OnEnable()
    {
        canMove = true;
        ResetEnemy();
        ResetCurrentTarget();
    }

    void FixedUpdate()
    {
        HandleFocus();
        if (type == ENEMY_TYPE.PLONGEUR)
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

        if ((CurrentTarget == Hero && magnitude >= AttackDistanceHero) || CurrentTarget == Monster && magnitude >= AttackDistanceMonster)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * MoveSpeed, direction.y * MoveSpeed);
        }
        else if (magnitude < AttackDistanceHero)
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
            nextAttackTime = Time.timeSinceLevelLoad + AttackSpeed;

            if (Vector3.Distance(transform.position, CurrentTarget.position) <= AttackDistanceHero + 0.1f)
            {
                Hero hero = CurrentTarget.GetComponent<Hero>();
                Monster monster = CurrentTarget.GetComponent<Monster>();
                if (hero)
                {
                    hero.ChangeLife(-Damage);
                }
                else if (monster)
                {
                    monster.ChangeLife(-Damage);
                }
            }
        }
    }


    void HandleFocus()
    {
        switch(type)
        {
            case ENEMY_TYPE.COMMIS:
            case ENEMY_TYPE.PLONGEUR:
                if (CurrentTarget == Monster)
                {
                    if (Vector2.Distance(transform.position, Hero.position) < DistanceChangementFocus)
                    {
                        CurrentTarget = Hero;
                    }
                }
                break;
            case ENEMY_TYPE.GOURMAND:
                if (CurrentTarget == Monster && Time.timeSinceLevelLoad < timeLastHit + GourmandTimeResetFocus)
                {
                    CurrentTarget = Hero;
                }
                else if (CurrentTarget == Hero && Time.timeSinceLevelLoad > timeLastHit + GourmandTimeResetFocus)
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
            LootManager.Instance.SpawnRandomLoot(transform);
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
            case ENEMY_TYPE.PLONGEUR:
            case ENEMY_TYPE.GOURMAND:
                CurrentTarget = Monster;
                break;
            case ENEMY_TYPE.GORDON:
                CurrentTarget = Hero;
                break;
        }
    }

    public void ChangeLife(int amount)
    {
        EnemyLife += amount;
    }

    public ENEMY_TYPE GetEnemyType()
    {
        return type;
    }

    public int GetLife()
    {
        return EnemyLife;
    }

    private void ResetEnemy()
    {
        DistanceChangementFocus = GameManager.Instance.ChangeFocusDistance;
        AttackDistanceHero = GameManager.Instance.EnemyHeroAttackDistance;
        AttackDistanceMonster = GameManager.Instance.EnemyMonsterAttackDistance;
        ResetCurrentTarget();

        switch(type)
        {
            case ENEMY_TYPE.COMMIS:
                EnemyLife = GameManager.Instance.CommisStartingLife;
                MoveSpeed = GameManager.Instance.CommisSpeed;
                AttackSpeed = GameManager.Instance.CommisAttackSpeed;
                Damage = GameManager.Instance.CommisDamage;
                break;
            case ENEMY_TYPE.PLONGEUR:
                EnemyLife = GameManager.Instance.PlongeurStartingLife;
                MoveSpeed = GameManager.Instance.PlongeurSpeed;
                AttackSpeed = GameManager.Instance.PlongeurAttackSpeed;
                Damage = GameManager.Instance.PlongeurDamage;
                break;
            case ENEMY_TYPE.GOURMAND:
                EnemyLife = GameManager.Instance.GourmandStartingLife;
                MoveSpeed = GameManager.Instance.GourmandSpeed;
                AttackSpeed = GameManager.Instance.GourmandAttackSpeed;
                Damage = GameManager.Instance.GourmandDamage;
                break;
            case ENEMY_TYPE.GORDON:
                EnemyLife = GameManager.Instance.GordonStartingLife;
                MoveSpeed = GameManager.Instance.GordonSpeed;
                AttackSpeed = GameManager.Instance.GordonAttackSpeed;
                Damage = GameManager.Instance.GordonDamage;
                break;
        }
    }

    public void SetType(ENEMY_TYPE t)
    {
        type = t;
    }
}
