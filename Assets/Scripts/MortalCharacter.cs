using UnityEngine;
using System.Collections;


public class MortalCharacter : MonoBehaviour
{


    public int life = 30;
    private int aoeDamage;
    public float aoeDamageRate = 1f;
    private float currentFiringDelay;

    void Start()
    {
        currentFiringDelay = aoeDamageRate;
    }

    void Update()
    {
        currentFiringDelay += Time.deltaTime;

        if (currentFiringDelay >= aoeDamageRate)
        {
            currentFiringDelay = 0;
            life -= aoeDamage;
        }
        
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void LoseLife(int damage)
    {
        --life;
    }

    public void SetAoeDamage(int damagePerSecond)
    {
        aoeDamage = damagePerSecond;
    }

}
