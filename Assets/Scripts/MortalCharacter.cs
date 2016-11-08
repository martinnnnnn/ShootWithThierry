using UnityEngine;
using System.Collections;


public class MortalCharacter : MonoBehaviour
{


    public int life = 30;
    private int aoeDamage;

    void Update()
    {
        life -= aoeDamage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void LoseLife(int damage)
    {
        --life;
    }

    public void SetAoeDamage(int damage)
    {
        aoeDamage = damage;
    }

}
