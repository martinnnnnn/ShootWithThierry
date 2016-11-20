using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{


    private int levelNumber;
    private int waveNumber;
    private float waveTime;
    private int commitQuantity;
    private int plongeurQuantity;
    private int gourmandQuantity;
    private int gordonQuantity;


    public bool isRightWave(int level, int wave)
    {
        return (level == levelNumber && wave == waveNumber);
    }

    



}
