using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


//|	0	0	5	1	0	0	0	0	PISTOL	100
//|	0	0	5	1	0	0	0	0	SNIPER	0
//|	0	0	5	2	0	0	0	0	SNIPER	0
//|	0	0	5	3	0	0	0	0	ROCKET	0
//|	0	0	5	4	0	0	0	0	ROCKET	0
//|	0	0	5	4	0	0	0	0	LIFE	0

public class GameInitManager : Singleton<GameInitManager>
{
    public string pathToWavesData;
    List<WaveData> waves;
    List<LootData> loots;

    public Button button;

    void Awake()
    {
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


    //public void StartGame()
    //{
    //    Destroy(button.gameObject);
    //    foreach (WaveData wave in waves)
    //    {
    //        StartCoroutine(StartWave(wave));
    //    }
    //    foreach (LootData loot in loots)
    //    {
    //        StartCoroutine(StartLoot(loot));
    //    }
    //}

    IEnumerator StartWave(WaveData data, float timeDecalage = 0f)
    {
        yield return new WaitForSeconds(data.timing);
        StartCoroutine(WavesManager.Instance.SpawnWave(data));
    }

    IEnumerator StartLoot(LootData data, float timeDecalage = 0f)
    {
        yield return new WaitForSeconds(data.timing);
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
                int.Parse(columns[10]),
                int.Parse(columns[4]) - 1);
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

    public void addLoot(float timing, LOOT_TYPE type, int amount, int spawn = 0)
    {
        LootData data = new LootData();
        data.timing = timing;
        data.type = type;
        data.amount = amount;
        data.spawn = spawn;
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
    public int spawn;
}

