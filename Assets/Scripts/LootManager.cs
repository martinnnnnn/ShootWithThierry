using UnityEngine;
using System.Collections.Generic;


public class LootManager : Singleton<LootManager>
{

    public GameObject lootPrefab;
    public Transform lifeSpawnPosition;
    public Transform ammoSpawnPosition;
    public float timeBetweenLifeSpawns = 30f;
    private float nextLifeSpawnsTime;
    public bool canSpawnLife;

    void Start()
    {
        canSpawnLife = true;
        nextLifeSpawnsTime = timeBetweenLifeSpawns;
    }

    void Update()
    {

        if (Time.timeSinceLevelLoad >= nextLifeSpawnsTime && canSpawnLife)
        {
            nextLifeSpawnsTime += timeBetweenLifeSpawns;
            SpawnLife();
        }
    }

    public void SpawnLoot(Transform spawn, LOOT_TYPE type, int amount)
    {
        if (spawn == null) spawn = ammoSpawnPosition;
        GameObject loot = Instantiate(lootPrefab, spawn) as GameObject;
        loot.GetComponent<Loot>().SetLoot(type,amount);
    }

    public void SpawnLoot(Transform spawn, LootData data)
    {
        if (spawn == null) spawn = ammoSpawnPosition;
        GameObject loot = Instantiate(lootPrefab, spawn) as GameObject;
        loot.GetComponent<Loot>().SetLoot(data.type, data.amount);
    }

    public void SpawnRandomLoot(Transform spawn, int amount)
    {
        if (spawn == null) spawn = ammoSpawnPosition;
        GameObject loot = Instantiate(lootPrefab, spawn) as GameObject;
        loot.GetComponent<Loot>().SetLoot((LOOT_TYPE)Random.Range(0,4), amount);
    }


    public void SpawnLife()
    {
        GameObject loot = Instantiate(lootPrefab, lifeSpawnPosition.position, new Quaternion()) as GameObject;
        loot.GetComponent<Loot>().SetLoot(LOOT_TYPE.SNIPER, 6);
        canSpawnLife = false;
    }
}
