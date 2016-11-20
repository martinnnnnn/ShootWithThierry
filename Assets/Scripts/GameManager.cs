using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : Singleton<GameManager>
{

    public string path;
    List<WaveData> waves;
    List<LootData> loots;

    void Start()
    {
        waves = new List<WaveData>();
        loots = new List<LootData>();

        loadWaveFile(path);
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
        WavesManager.Instance.SpawnWave(data);
    }

    IEnumerator StartLoot(LootData data)
    {
        yield return new WaitForSeconds(data.timing - Time.timeSinceLevelLoad);
        LootManager.Instance.SpawnLoot(null, data);
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
            if (columns[0] != "0")
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
                float.Parse(columns[2]),
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