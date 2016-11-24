using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Monster : MonoBehaviour
{
    private int MonsterLife;

    private int cacStage;
    private int lavaStage;
    private int hellStage;
    private int cacDamage;
    private float timeBetweenCac;
    private float timeBetweenLava;
    private float timeBetweenHell;
    private float timeLastCac;
    private float timeLastLava;
    private float timeLastHell;
    private float cacRadius;

    public int numberOfHellWaves = 10;

    private List<Transform> cacStagePlaces;
    public GameObject lavaPrefab;

    private List<Vector2> rocketBulletsDirections1;
    private List<Vector2> rocketBulletsDirections2;

    private bool temp = false;


    void Awake()
    {
        MonsterLife = GameDataManager.Instance.MonsterStartingLife;
        cacStage = GameDataManager.Instance.MonsterCaCStage;
        lavaStage = GameDataManager.Instance.MonsterLavaStage;
        hellStage = GameDataManager.Instance.MonsterHellStage;
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

        UIManager.Instance.SetMonsterLife(MonsterLife);
    }

    void Update()
    {
        Attack();

        //if (!temp)
        //{
        //    StartCoroutine(HellAttack());
        //    temp = true;
        //}
    }

    private void Attack()
    {
        if (MonsterLife < cacStage && MonsterLife > lavaStage)
        {
            if (Time.timeSinceLevelLoad > timeLastCac + timeBetweenCac)
            {
                timeLastCac = Time.timeSinceLevelLoad;
                CaCAttack();
            }
        }
        else if (MonsterLife < lavaStage && MonsterLife > hellStage)
        {
            if (Time.timeSinceLevelLoad > timeLastLava + timeBetweenLava)
            {
                timeLastLava = Time.timeSinceLevelLoad;
                LavaAttack();
            }
        }
        else if (MonsterLife < hellStage && MonsterLife > 0)
        {
            if (Time.timeSinceLevelLoad > timeLastHell + timeBetweenHell)
            {
                timeLastHell = Time.timeSinceLevelLoad;
                StartCoroutine(HellAttack());
            }
        }
    }

    private void CaCAttack()
    {
        Debug.Log("cacDamage");
        Collider2D[] attackedObjects =  Physics2D.OverlapCircleAll(transform.position, cacRadius);
        for (int i = 0; i < attackedObjects.Length; ++i)
        {
            if (attackedObjects[i].GetComponent<Enemy>() || attackedObjects[i].GetComponent<Hero>())
            {
                attackedObjects[i].gameObject.SendMessage("ChangeLife", -cacDamage);
            }
        }
    }

    private IEnumerator HellAttack()
    {
        Vector3 localShotPos = new Vector3(0, -((new Vector2(transform.localScale.x * 8f,
                                    transform.localScale.y * 5f)).magnitude));

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

    private void LavaAttack()
    {
        foreach (Transform t in cacStagePlaces)
        {
            GameObject lava = ObjectPool.GetNextObject(lavaPrefab);
           // GameObject go = Instantiate(lavaPrefab, t.position, t.rotation) as GameObject;
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
        UIManager.Instance.SetMonsterLife(MonsterLife);
    }

}
