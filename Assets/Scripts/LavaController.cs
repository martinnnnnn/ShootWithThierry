using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class LavaController : MonoBehaviour
{

    public float duration = 20f;
    public int damagePerSecond = 2;

    void Start()
    {

    }
    

    void OnTriggerEnter2D(Collider2D c)
    {
        MortalCharacter mC = c.gameObject.GetComponent<MortalCharacter>();
        if (mC != null)
        {
            mC.AddAoeDamage(damagePerSecond);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        MortalCharacter mC = c.gameObject.GetComponent<MortalCharacter>();
        if (mC != null)
        {
            mC.RemoveAoeDamage(damagePerSecond);
        }
    }


}
