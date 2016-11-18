using UnityEngine;
using System.Collections;


public enum LOOT_TYPE
{
    PISTOL,
    SNIPER,
    ROCKET,
    LIFE,
}


public class Loot : MonoBehaviour
{
    private LOOT_TYPE dropType;
    private int amount;

    void Start ()
    {
        
    }


    void SetLoot(LOOT_TYPE type, int a)
    {
        dropType = type;
        amount = a;
    }

    public LOOT_TYPE GetLootType()
    {
        return dropType;
    }

    public int GetLootAmount()
    {
        return amount;
    }
}
