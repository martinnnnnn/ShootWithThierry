using UnityEngine;
using System.Collections.Generic;


public class DOTDamage : MonoBehaviour
{
    private Hero hero;
    private Monster monster;
    private Enemy enemy;
    private float TimeBetweenDamage;
    private int Damage;
    private float ExpirationTime;

    private float TimeNextDamage;

    void Awake()
    {
        TimeNextDamage = 0;
        hero = GetComponent<Hero>();
        monster = GetComponent<Monster>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= ExpirationTime)
        {
            Destroy(this);
        }

        if (Time.timeSinceLevelLoad >= TimeNextDamage)
        {
            TimeNextDamage = Time.timeSinceLevelLoad + TimeBetweenDamage;
            if (hero)
            {
                hero.ChangeLife(-Damage);
            }
            if (monster)
            {
                monster.ChangeLife(-Damage);
            }
            if (enemy)
            {
                enemy.ChangeLife(-Damage);
            }
        }
    }
    
    public void SetDOT(float duration, float timebetweendamage, int dmg)
    {
        ExpirationTime = Time.timeSinceLevelLoad + duration;
        TimeBetweenDamage = timebetweendamage;
        Damage = dmg;
    }

}
