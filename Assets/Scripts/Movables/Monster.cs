using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;


public enum MONSTER_STAGES
{
    STAGE1,
    STAGE2,
    STAGE3
}



public class Monster : MonoBehaviour
{
    private int MonsterLife;


    private MONSTER_STAGES currentStage;


    private int cacStage;
    private int standardHellStage;
    private int fancyHellStage;
    private int cacDamage;
    private float timeBetweenCac;
    private float timeBetweenLava;
    private float timeBetweenHell;
    private float timeLastCac;
    private float timeLastLava;
    private float timeLastHell;
    private float cacRadius;

    private int numberOfHellWaves = 25;

    private List<Transform> cacStagePlaces;
    public GameObject lavaPrefab;

    private List<Vector2> rocketBulletsDirections1;
    private List<Vector2> rocketBulletsDirections2;

    private bool temp = false;

    public Animator anim;

    void Awake()
    {
        MonsterLife = GameDataManager.Instance.MonsterStartingLife;
        cacStage = GameDataManager.Instance.MonsterCaCStage;
        standardHellStage = GameDataManager.Instance.MonsterHellStage;
        fancyHellStage = GameDataManager.Instance.MonsterLavaStage;
         timeBetweenCac = GameDataManager.Instance.MonsterTimeBetweenCaCAttacks;
        timeBetweenLava = GameDataManager.Instance.MonsterTimeBetweenLavaAttacks;
        timeBetweenHell = GameDataManager.Instance.MonsterTimeBetweenHellAttacks;
        cacRadius = GameDataManager.Instance.MonsterCaCRadius;
        cacDamage = GameDataManager.Instance.MonsterCaCDamage;
        lavaPrefab = GameDataManager.Instance.Lava;

        cacStagePlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            cacStagePlaces.Add(t);
        }

        InitBulletSpawns();

        timeLastCac = 0f;
        timeLastLava = 0f;
        timeLastHell = 0f;

        SetCurrentStage();

        UIManager.Instance.SetMonsterLife(MonsterLife);

        SetAnimIdle();
    }

    void Update()
    {
        Attack();

        //if (!temp)
        //{
        //    StartCoroutine(FancyHellAttack());
        //    temp = true;
        //}
    }

    private void Attack()
    {
        switch (currentStage)
        {
            case MONSTER_STAGES.STAGE1:
                if (Time.timeSinceLevelLoad > timeLastCac + timeBetweenCac)
                {
                    timeLastCac = Time.timeSinceLevelLoad;
                    CaCAttack();
                }
                break;
            case MONSTER_STAGES.STAGE2:
                if (Time.timeSinceLevelLoad > timeLastLava + timeBetweenLava)
                {
                    timeLastLava = Time.timeSinceLevelLoad;
                    StartCoroutine(StandardHellAtatck());
                }
                break;
            case MONSTER_STAGES.STAGE3:
                if (Time.timeSinceLevelLoad > timeLastHell + timeBetweenHell)
                {
                    timeLastHell = Time.timeSinceLevelLoad;
                    StartCoroutine(FancyHellAttack());
                }
                break;
        }

        //if (MonsterLife < cacStage)
        //{
        //    if (Time.timeSinceLevelLoad > timeLastCac + timeBetweenCac)
        //    {
        //        timeLastCac = Time.timeSinceLevelLoad;
        //        CaCAttack();
        //    }
        //}
        //if (MonsterLife < standardHellStage)
        //{
        //    if (Time.timeSinceLevelLoad > timeLastLava + timeBetweenLava)
        //    {
        //        timeLastLava = Time.timeSinceLevelLoad;
        //        StartCoroutine(StandardHellAtatck());
        //    }
        //}
        //if (MonsterLife < fancyHellStage)
        //{
        //    if (Time.timeSinceLevelLoad > timeLastHell + timeBetweenHell)
        //    {
        //        timeLastHell = Time.timeSinceLevelLoad;
        //        StartCoroutine(FancyHellAttack());
        //    }
        //}
    }

    private void CaCAttack()
    {
        Collider2D[] attackedObjects =  Physics2D.OverlapCircleAll(transform.position, cacRadius);
        for (int i = 0; i < attackedObjects.Length; ++i)
        {
            if (attackedObjects[i].GetComponent<Enemy>() || attackedObjects[i].GetComponent<Hero>())
            {
                attackedObjects[i].gameObject.SendMessage("ChangeLife", -cacDamage);
            }
        }
    }

    private float angle = 1f;
    private int count = 20;
    private float radius = 2f;
    private int mult = 1;
    private IEnumerator FancyHellAttack()
    {
        for (int i = 0; i < numberOfHellWaves; ++i)
        {
            for (int j = 0; j < count; j++)
            {
                Vector3 thispos = new Vector3(radius * (float)Math.Sin(angle), radius * (float)Math.Cos(angle) * mult, 0);
                thispos.Normalize();
                BulletManager.Instance.FireBullet(WEAPON_TYPE.MONSTER, transform, thispos,gameObject);
                angle += (2 * (float)Math.PI) / count;
            }
            angle += 15;

            yield return new WaitForSeconds(.15f);
        }


    }


    private IEnumerator StandardHellAtatck()
    {
        //Vector3 localShotPos = new Vector3(0, -((new Vector2(transform.localScale.x * 8f,
        //                            transform.localScale.y * 5f)).magnitude));

        for (int i = 0; i < numberOfHellWaves; ++i)
        {
            foreach (Vector2 direction in rocketBulletsDirections1)
            {
                BulletManager.Instance.FireBullet(WEAPON_TYPE.MONSTER, transform, direction, gameObject);
            }
            yield return new WaitForSeconds(.5f);

            foreach (Vector2 direction in rocketBulletsDirections2)
            {
                BulletManager.Instance.FireBullet(WEAPON_TYPE.MONSTER, transform, direction, gameObject);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private void InitBulletSpawns()
    {
        rocketBulletsDirections1 = new List<Vector2>();
        rocketBulletsDirections1.Add(new Vector2(0, 1));
        rocketBulletsDirections1.Add(new Vector2(0, -1));
        rocketBulletsDirections1.Add(new Vector2(1, 0));
        rocketBulletsDirections1.Add(new Vector2(-1, 0));
        rocketBulletsDirections1.Add(new Vector2(.5f, .5f));
        rocketBulletsDirections1.Add(new Vector2(-.5f, .5f));
        rocketBulletsDirections1.Add(new Vector2(.5f, -.5f));
        rocketBulletsDirections1.Add(new Vector2(-.5f, -.5f));

        float sqrtAngle = Mathf.Sqrt(3) / 2;
        rocketBulletsDirections2 = new List<Vector2>();
        rocketBulletsDirections2.Add(new Vector2(.5f, sqrtAngle));
        rocketBulletsDirections2.Add(new Vector2(.5f, -sqrtAngle));
        rocketBulletsDirections2.Add(new Vector2(-.5f, sqrtAngle));
        rocketBulletsDirections2.Add(new Vector2(-.5f, -sqrtAngle));
        rocketBulletsDirections2.Add(new Vector2(sqrtAngle, .5f));
        rocketBulletsDirections2.Add(new Vector2(sqrtAngle, -.5f));
        rocketBulletsDirections2.Add(new Vector2(-sqrtAngle, .5f));
        rocketBulletsDirections2.Add(new Vector2(-sqrtAngle, -.5f));
    }

    public void ChangeLife(int amount)
    {
        
        MonsterLife += amount;
        if (MonsterLife > GameDataManager.Instance.MonsterMaxLife) MonsterLife = GameDataManager.Instance.MonsterMaxLife;
        if (MonsterLife <= 0)
        {
            SceneManager.LoadScene("testTime");
        }

        SetCurrentStage();

        UIManager.Instance.SetMonsterLife(MonsterLife);
        SetAnimIdle();
    }

    
    void SetCurrentStage()
    {
        
        if (MonsterLife <= fancyHellStage)
        {
            currentStage = MONSTER_STAGES.STAGE3;
        }
        else if (MonsterLife <= standardHellStage)
        {
            currentStage = MONSTER_STAGES.STAGE2;
        }
        else
        {
            currentStage = MONSTER_STAGES.STAGE1;
        }
        Debug.Log("life:" + MonsterLife + "/stage:"+currentStage);
    }


    void SetAnimIdle()
    {
        if (MonsterLife > fancyHellStage)
        {
            anim.SetTrigger("ToIdle1");
        }
        if (MonsterLife < fancyHellStage)
        {
            anim.SetTrigger("ToIdle2");
        }
        if (MonsterLife < standardHellStage)
        {
            anim.SetTrigger("ToIdle3");
        }
    }

}
