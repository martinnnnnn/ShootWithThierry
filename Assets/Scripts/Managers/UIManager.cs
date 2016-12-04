using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class UIManager : Singleton<UIManager>
{
    public Text HeroLife;
    public Text MonsterLife;
    public Text Ammo;
    public Text Timer;

    void Update()
    {
        Timer.text = "Time : " + Time.timeSinceLevelLoad;
    }

    public void SetHeroLife(int value)
    {
        HeroLife.text = "Hero Life : " + value;
    }

    public void SetMonsterLife(int value)
    {
        //MonsterLife.text = "Monster Life : " + value;
    }

    public void SetAmmo(int value)
    {
        Ammo.text = "Ammo : " + value;
    }

}
