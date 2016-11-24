using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Lava : MonoBehaviour
{

    private float LavaDuration;
    private int LavaDamage;
    private float LavaTimeBetweenAttack;

    private float deactivationTime;

    void Awake()
    {
        LavaDuration = GameDataManager.Instance.LavaDuration;
        LavaDamage = GameDataManager.Instance.LavaDamage;
        LavaTimeBetweenAttack = GameDataManager.Instance.LavaTimeBetweenAttack;

        deactivationTime = Time.timeSinceLevelLoad + LavaDuration;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= deactivationTime)
        {
            gameObject.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        DOTDamage dot = c.gameObject.AddComponent<DOTDamage>();
        dot.SetDOT(LavaDuration, LavaTimeBetweenAttack, LavaDamage);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Destroy(c.GetComponent<DOTDamage>());
    }
}
