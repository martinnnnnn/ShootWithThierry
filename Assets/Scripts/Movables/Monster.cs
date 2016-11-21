using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Monster : MonoBehaviour
{
    private int MonsterLife;

    private int cacStage;
    private int lavaStage;
    private int hellStage;
    private float timeBetweenCac;
    private float timeBetweenLava;
    private float timeBetweenHell;
    private float timeLastCac;
    private float timeLastLava;
    private float timeLastHell;


    public int numberOfHellWaves = 10;

    private List<Transform> cacStagePlaces;
    public GameObject lava;

    private List<Vector2> rocketBulletsDirections1;
    private List<Vector2> rocketBulletsDirections2;

    private bool temp = false;


    void Awake()
    {
        MonsterLife = GameManager.Instance.MonsterStartingLife;
        cacStage = GameManager.Instance.MonsterCaCStage;
        lavaStage = GameManager.Instance.MonsterLavaStage;
        hellStage = GameManager.Instance.MonsterHellStage;
        timeBetweenCac = GameManager.Instance.MonsterTimeBetweenCaCAttacks;
        timeBetweenLava = GameManager.Instance.MonsterTimeBetweenLavaAttacks;
        timeBetweenHell = GameManager.Instance.MonsterTimeBetweenHellAttacks;

        cacStagePlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            cacStagePlaces.Add(t);
        }

        InitBulletSpawns();

        timeLastCac = 0f;
        timeLastLava = 0f;
        timeLastHell = 0f;
}

    void Update()
    {
        //if (myLife.life < firstStage && myLife.life > secondStage)
        //{
        //    CaCAttack();
        //}
        //else if (myLife.life < secondStage && myLife.life > thirdSage)
        //{
        //    //LavaAttack();
        //}
        //else if (myLife.life < thirdSage)
        //{
        //    if (!temp)
        //    {
        //        StartCoroutine(BulletHellAttack());
        //        temp = true;
        //    }
        //}

        if (!temp)
        {
            StartCoroutine(HellAttack());
            temp = true;
        }
    }

    private void CaCAttack()
    {

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
            GameObject go = Instantiate(lava, t.position, t.rotation) as GameObject;
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
                HellAttack();
            }
        }
    }

    public void ChangeLife(int amount)
    {
        MonsterLife += amount;
    }

}
