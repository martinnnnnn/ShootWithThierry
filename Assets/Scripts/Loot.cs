﻿using UnityEngine;
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
    private LOOT_TYPE lootType;
    private int amount;

    void Awake()
    {
    }


    public void SetLoot(LOOT_TYPE type, int a)
    {
        lootType = type;
        amount = a;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LOOT/IMG_LOOT_" + lootType.ToString());
        gameObject.AddComponent<BoxCollider2D>();
    }

    public LOOT_TYPE GetLootType()
    {
        return lootType;
    }

    public int GetLootAmount()
    {
        return amount;
    }
}
