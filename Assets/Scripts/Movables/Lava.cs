using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Lava : MonoBehaviour
{

    private float LavaDuration;
    private int LavaDamage;
    private float LavaTimeBetweenAttack;

    void Awake()
    {
        LavaDuration = GameManager.Instance.LavaDuration;
        LavaDamage = GameManager.Instance.LavaDamage;
        LavaTimeBetweenAttack = GameManager.Instance.LavaTimeBetweenAttack;
    }
    

    void OnTriggerEnter2D(Collider2D c)
    {
        c.gameObject.AddComponent<DOTDamage>();
        c.gameObject.GetComponent<DOTDamage>().SetDOT(LavaDuration, LavaTimeBetweenAttack, LavaDamage);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        
    }


}
