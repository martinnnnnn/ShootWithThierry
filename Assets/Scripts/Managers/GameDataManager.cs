using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameDataManager : Singleton<GameDataManager>
{

    public string pathToWavesData;
    List<WaveData> waves;
    List<LootData> loots;

    [Space(3)]
    [Header("Bullets")]
    public int StartingAmmo;
    public int PistolAmmoPerShot;
    public int SniperAmmoPerShot;
    public int RocketAmmoPerShot;
    public float PistolFireRate;
    public float SniperFireRate;
    public float RocketFireRate;
    public float PistolSpeed;
    public float SniperSpeed;
    public float RocketSpeed;
    public float FragmentSpeed;
    public float MonsterBulletSpeed;
    public int PistolDamage;
    public int SniperDamage;
    public int RocketDamage;
    public int FragmentDamage;
    public int MonsterBulletDamage;
    public float RocketTimeBeforeExplosion;

    [Space(3)]
    [Header("Hero")]
    public float HeroSpeed;
    public int HeroStartingLife;

    [Space(3)]
    [Header("Dash")]
    public float HeroDashSpeed;
    public float PlongeurDashSpeed;
    public float HeroDashCoolDown;
    public float PlongeurDashCoolDown;

    [Space(3)]
    [Header("Monster")]
    public int MonsterStartingLife;
    public int MonsterCaCStage;
    public int MonsterLavaStage;
    public int MonsterHellStage;
    public int MonsterTimeBetweenCaCAttacks;
    public int MonsterTimeBetweenLavaAttacks;
    public int MonsterTimeBetweenHellAttacks;
    public float MonsterCaCRadius;
    public int MonsterCaCDamage;

    [Space(3)]
    [Header("Lava")]
    public float LavaDuration;
    public int LavaDamage;
    public float LavaTimeBetweenAttack;

    [Space(3)]
    [Header("Enemy")]
    public int CommisStartingLife;
    public int PlongeurStartingLife;
    public int GourmandStartingLife;
    public int GordonStartingLife;

    public float CommisSpeed;
    public float PlongeurSpeed;
    public float GourmandSpeed;
    public float GordonSpeed;

    public float CommisAttackSpeed;
    public float PlongeurAttackSpeed;
    public float GourmandAttackSpeed;
    public float GordonAttackSpeed;

    public int CommisDamage;
    public int PlongeurDamage;
    public int GourmandDamage;
    public int GordonDamage;

    public float ChangeFocusDistance;
    public float EnemyMonsterAttackDistance;
    public float EnemyHeroAttackDistance;


    public int GordonLootPistolAmount;
    public int GordonLootSniperAmount;
    public int GordonLootRocketAmount;

    public float GourmandTimeResetFocus;

    [HideInInspector]
    public Transform Hero;
    [HideInInspector]
    public Transform Monster;
    [HideInInspector]
    public GameObject Lava;
    [HideInInspector]
    public GameObject Bullet;
    [HideInInspector]
    public GameObject Loot;
    [HideInInspector]
    public GameObject Commis;
    [HideInInspector]
    public GameObject Plongeur;
    [HideInInspector]
    public GameObject Gourmand;
    [HideInInspector]
    public GameObject Gordon;

    void Awake()
    {
        Hero = transform.Find("Hero");
        Monster = transform.Find("Monster");
        Lava = Resources.Load("PREFABS/Lava") as GameObject;
        Bullet = Resources.Load("PREFABS/Bullet") as GameObject;
        Loot = Resources.Load("PREFABS/Loot") as GameObject;
        Commis = Resources.Load("PREFABS/ENEMIES/Commis") as GameObject;
        Plongeur = Resources.Load("PREFABS/ENEMIES/Plongeur") as GameObject;
        Gourmand = Resources.Load("PREFABS/ENEMIES/Gourmand") as GameObject;
        Gordon = Resources.Load("PREFABS/ENEMIES/Gordon") as GameObject;

        waves = new List<WaveData>();
        loots = new List<LootData>();

        loadWaveFile(pathToWavesData);
        foreach (WaveData wave in waves)
        {
            StartCoroutine(StartWave(wave));
        }
        foreach (LootData loot in loots)
        {
            StartCoroutine(StartLoot(loot));
        }
    }

    IEnumerator StartWave(WaveData data)
    {
        yield return new WaitForSeconds(data.timing - Time.timeSinceLevelLoad);
        StartCoroutine(WavesManager.Instance.SpawnWave(data));
    }

    IEnumerator StartLoot(LootData data)
    {
        yield return new WaitForSeconds(data.timing - Time.timeSinceLevelLoad);
        LootManager.Instance.SpawnLoot(data);
    }



    private void loadWaveFile(string path)
    {
       
        // Load resources
        TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
        if (textAsset == null)
        {
            Debug.LogWarning("[TextManager] " + path + " file not found.");
            return;
        }

        Debug.Log("[TextManager] loading: " + path);

        // Read the file
        string[] lineSeparators = new string[] { "|" };
        string[] lines = textAsset.text.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);

        string[] columnSeparators = new string[] { "\t" };
        string[] columns;



        for (int i = 1; i < lines.Length; i++)
        {
            columns = lines[i].Split(columnSeparators, System.StringSplitOptions.None);
            if (columns[1] != "0")
            {
                addWave(
                float.Parse(columns[3]),
                int.Parse(columns[4]) - 1,
                int.Parse(columns[5]),
                int.Parse(columns[6]),
                int.Parse(columns[7]),
                int.Parse(columns[8]));
            }
            else
            {
                addLoot(
                float.Parse(columns[3]),
                (LOOT_TYPE)Enum.Parse(typeof(LOOT_TYPE), columns[9], true),
                int.Parse(columns[10]));
            }
        }
    }

    public void addWave(float timing, int position, int commit, int plongeur, int gourmand, int gordon)
    {
        WaveData data = new WaveData();
        data.timing = timing;
        data.spawningPosition = position;
        data.commitQuantity = commit;
        data.plongeurQuantity = plongeur;
        data.gourmandQuantity = gourmand;
        data.gordonQuantity = gordon;
        waves.Add(data);
    }

    public void addLoot(float timing, LOOT_TYPE type, int amount)
    {
        LootData data = new LootData();
        data.timing = timing;
        data.type = type;
        data.amount = amount;
        loots.Add(data);
    }
}



public struct WaveData
{
    public float timing;
    public int spawningPosition;
    public int commitQuantity;
    public int plongeurQuantity;
    public int gourmandQuantity;
    public int gordonQuantity;

    override public string ToString()
    {
        return ("" + timing
            + ":" + spawningPosition
            + ":" + commitQuantity
            + ":" + plongeurQuantity
            + ":" + gourmandQuantity
            + ":" + gordonQuantity);
    }
}

public struct LootData
{
    public float timing;
    public LOOT_TYPE type;
    public int amount;
}