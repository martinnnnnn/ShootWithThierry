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

    public GameObject bloodPrefab;

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
    private float timeUnmovable = 0.1f;
    private float nextAttackTime;

    private float GourmandTimeResetFocus;
    private float timeLastHit;

    // private float tooCloseEnemies = 1f;
    private bool isColliding = false;

    public Animator anim;
    private bool facingLeft = true;
    private bool isWalking = false;

    void Awake()
    {
        Monster = GameDataManager.Instance.Monster;
        Hero = GameDataManager.Instance.Hero;
        GourmandTimeResetFocus = GameDataManager.Instance.GourmandTimeResetFocus;
    }

    void OnEnable()
    {
        canMove = true;
        ResetEnemy();
        ResetCurrentTarget();
    }

    void Update()
    {
        if (EnemyLife <= 0)
        {
            CFX_SpawnSystem.GetNextObject(bloodPrefab);
            bloodPrefab.transform.position = transform.position;
            //Debug.LogError("this:" + transform.position + " /fx:" + bloodPrefab.transform.position);
            DeapthManager.Instance.RemoveActor(gameObject);
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        HandleFocus();
        if (type == ENEMY_TYPE.PLONGEUR)
        {
            HandleDash();
        }
        if (canMove) Move();
        Attack();
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject == CurrentTarget.gameObject)
        {
            isColliding = true;
        }
            
    }

    public void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject == CurrentTarget.gameObject)
        {
            isColliding = false;
        }
    }


    void Move()
    {

        Vector3 direction = CurrentTarget.position - transform.position;
        float distance = Vector3.Distance(CurrentTarget.position,transform.position);
        direction.Normalize();

        if (!isColliding)
       // if ((CurrentTarget == Hero && distance >= AttackDistanceHero) || CurrentTarget == Monster && distance >= AttackDistanceMonster)
        {
            isWalking = true;
            GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * MoveSpeed, direction.y * MoveSpeed);
        }
        else
        //else if ((CurrentTarget == Hero && distance < AttackDistanceHero) || CurrentTarget == Monster && distance < AttackDistanceMonster)
        {
            isWalking = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }

        anim.SetBool("isWalking", isWalking);
        anim.SetFloat("hSpeed", Mathf.Abs(direction.x));
        anim.SetFloat("vSpeed", direction.y);

        if (direction.x > 0 && facingLeft)
        {
            Flip();
        }
        else if (direction.x < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Attack()
    {
        if (Time.timeSinceLevelLoad > nextAttackTime)
        {
            nextAttackTime = Time.timeSinceLevelLoad + AttackSpeed;
            float distance = Vector3.Distance(transform.position, CurrentTarget.position);
            //Debug.Log("distance :" + distance);
            if (isColliding)
            //if (CurrentTarget == Hero && distance <= AttackDistanceHero + 0.1f
            //    || CurrentTarget == Monster && distance <= AttackDistanceMonster + 0.1f)
            {
                anim.SetTrigger("Attack");
                //Hero hero = CurrentTarget.GetComponent<Hero>();
                //Monster monster = CurrentTarget.GetComponent<Monster>();
                CurrentTarget.gameObject.SendMessage("ChangeLife", -Damage);
                //if (hero)
                //{
                //    hero.ChangeLife(-Damage);
                //}
                //else if (monster)
                //{
                //    monster.ChangeLife(-Damage);
                //}
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


    //public void DropLoot()
    //{
    //    if (type == ENEMY_TYPE.GORDON)
    //    {
    //        LootManager.Instance.SpawnRandomLoot(transform);
    //    }
    //}


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
        DistanceChangementFocus = GameDataManager.Instance.ChangeFocusDistance;
        AttackDistanceHero = GameDataManager.Instance.EnemyHeroAttackDistance;
        AttackDistanceMonster = GameDataManager.Instance.EnemyMonsterAttackDistance;
        ResetCurrentTarget();

        switch(type)
        {
            case ENEMY_TYPE.COMMIS:
                EnemyLife = GameDataManager.Instance.CommisStartingLife;
                MoveSpeed = GameDataManager.Instance.CommisSpeed;
                AttackSpeed = GameDataManager.Instance.CommisAttackSpeed;
                Damage = GameDataManager.Instance.CommisDamage;
                break;
            case ENEMY_TYPE.PLONGEUR:
                EnemyLife = GameDataManager.Instance.PlongeurStartingLife;
                MoveSpeed = GameDataManager.Instance.PlongeurSpeed;
                AttackSpeed = GameDataManager.Instance.PlongeurAttackSpeed;
                Damage = GameDataManager.Instance.PlongeurDamage;
                break;
            case ENEMY_TYPE.GOURMAND:
                EnemyLife = GameDataManager.Instance.GourmandStartingLife;
                MoveSpeed = GameDataManager.Instance.GourmandSpeed;
                AttackSpeed = GameDataManager.Instance.GourmandAttackSpeed;
                Damage = GameDataManager.Instance.GourmandDamage;
                break;
            case ENEMY_TYPE.GORDON:
                EnemyLife = GameDataManager.Instance.GordonStartingLife;
                MoveSpeed = GameDataManager.Instance.GordonSpeed;
                AttackSpeed = GameDataManager.Instance.GordonAttackSpeed;
                Damage = GameDataManager.Instance.GordonDamage;
                break;
        }
    }

    public void SetType(ENEMY_TYPE t)
    {
        type = t;
        ResetEnemy();
    }

    private void AnimateDash()
    {
        anim.SetTrigger("Dash");
    }
}
