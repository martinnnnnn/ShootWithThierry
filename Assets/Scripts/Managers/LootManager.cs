using UnityEngine;
using System.Collections.Generic;


public class LootManager : Singleton<LootManager>
{

    private GameObject lootPrefab;
    public LootSpawn lifeSpawnPosition;
    public LootSpawn recipeSpawnPosition;
    public LootSpawn[] AmmoSpawnPosition;

    void Start()
    {
        lootPrefab = GameDataManager.Instance.Loot;
    }


    public void SpawnLoot(LootData data)
    {
        LootSpawn lootspawn = lifeSpawnPosition;
        switch (data.type)
        {
            case LOOT_TYPE.LIFE:
                lootspawn = lifeSpawnPosition;
                break;
            case LOOT_TYPE.ROCKET:
            case LOOT_TYPE.SNIPER:
                lootspawn = recipeSpawnPosition;
                break;
            case LOOT_TYPE.PISTOL:
                lootspawn = AmmoSpawnPosition[data.spawn];
                break;
        }
        GameObject loot = ObjectPool.GetNextObject(lootPrefab, lootspawn.transform);
        loot.GetComponent<Loot>().SetLoot(data.type, data.amount);
        lootspawn.CurrentLoot = loot;
    }


    //void Update()
    //{

    //if (Time.timeSinceLevelLoad >= nextLifeSpawnsTime)
    //{
    //    nextLifeSpawnsTime += timeBetweenLifeSpawns;
    //    SpawnLife();
    //}
    //}

    //public void SpawnLoot(Transform spawn, LOOT_TYPE type, int amount)
    //{
    //    if (spawn == null) spawn = recipeSpawnPosition;
    //    GameObject loot = ObjectPool.GetNextObject(lootPrefab,spawn);
    //    //GameObject loot = Instantiate(lootPrefab, spawn) as GameObject;
    //    loot.GetComponent<Loot>().SetLoot(type,amount);
    //}



    //public void SpawnRandomLoot(Transform spawn, int amount = 0)
    //{
    //    if (spawn == null) spawn = recipeSpawnPosition;
    //    LOOT_TYPE type = (LOOT_TYPE)Random.Range(0, 4);
    //    if (amount == 0) amount = GetAmount(type);
    //    //GameObject loot = Instantiate(lootPrefab, spawn) as GameObject;
    //    GameObject loot = ObjectPool.GetNextObject(lootPrefab, spawn);
    //    loot.GetComponent<Loot>().SetLoot((LOOT_TYPE)Random.Range(0,4), amount);
    //}


    //public void SpawnLife()
    //{
    //   // GameObject loot = Instantiate(lootPrefab, lifeSpawnPosition.position, new Quaternion()) as GameObject;
    //    GameObject loot = ObjectPool.GetNextObject(lootPrefab, lifeSpawnPosition);
    //    loot.GetComponent<Loot>().SetLoot(LOOT_TYPE.SNIPER, 6);
    //}

    //private int GetAmount(LOOT_TYPE type)
    //{
    //    int amount = 0;
    //    switch(type)
    //    {
    //        case LOOT_TYPE.PISTOL:
    //            amount = GameDataManager.Instance.GordonLootPistolAmount;
    //            break;
    //        case LOOT_TYPE.SNIPER:
    //            amount = GameDataManager.Instance.GordonLootSniperAmount;
    //            break;
    //        case LOOT_TYPE.ROCKET:
    //            amount = GameDataManager.Instance.GordonLootRocketAmount;
    //            break;
    //    }
    //    return amount;
    //}
}
