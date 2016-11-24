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
        LavaDuration = GameDataManager.Instance.LavaDuration;
        LavaDamage = GameDataManager.Instance.LavaDamage;
        LavaTimeBetweenAttack = GameDataManager.Instance.LavaTimeBetweenAttack;
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
