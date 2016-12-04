using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class UIManager : Singleton<UIManager>
{

    public Text Timer;

    public Image HeroLife;
    public Sprite[] HeroLifeSprites;

    public Image Ammo;
    private List<Sprite> AmmoSprites;

    public Image Recipe;
    public Sprite RecipeSniper;
    public Sprite RecipeRocket;

    public Image MonsterJaugeColere;
    private List<Sprite> MonsterJaugeColorSprites;
    public Image MonsterFace;
    private List<Sprite> MonsterFaceSprites;


    void Awake()
    {
        MonsterJaugeColorSprites = new List<Sprite>();
        for (int i = 0; i < 50; ++i)
        {
            if (i < 10)
            {
                MonsterJaugeColorSprites.Add(Resources.Load<Sprite>("HUD/MONSTER/jaugecolere_0000" + i));
            }
            else
            {
                MonsterJaugeColorSprites.Add(Resources.Load<Sprite>("HUD/MONSTER/jaugecolere_000" + i));
            }
        }

        MonsterFaceSprites = new List<Sprite>();
        for (int i = 0; i < 10; ++i)
        {
            MonsterFaceSprites.Add(Resources.Load<Sprite>("HUD/MONSTER/flan_0" + i));

        }

        AmmoSprites = new List<Sprite>();
        for (int i = 0; i < 50; ++i)
        {
            if (i < 10)
            {
                AmmoSprites.Add(Resources.Load<Sprite>("HUD/HERO/chargeur_000" + i));
            }
            else
            {
                AmmoSprites.Add(Resources.Load<Sprite>("HUD/HERO/chargeur_00" + i));
            }
        }
    }

    void Update()
    {
        Timer.text = "Time : " + Time.timeSinceLevelLoad;

        float minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60);
        float seconds = Mathf.RoundToInt(Time.timeSinceLevelLoad % 60);

        string sminutes;
        string ssecondes;
        if(minutes < 10)
        {
            sminutes = "0" + minutes.ToString();
        }
        else
        {
            sminutes = minutes.ToString();
        }
        if (seconds < 10)
        {
            ssecondes = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            ssecondes = Mathf.RoundToInt(seconds).ToString();
        }
        Timer.text = sminutes + ":" + ssecondes; 
    }

    public void SetHeroLife(int value)
    {
        HeroLife.sprite = HeroLifeSprites[value / 2];
    }

    public void SetMonsterLife(int value)
    {
        MonsterJaugeColere.sprite = MonsterJaugeColorSprites[value/6];
        MonsterFace.sprite = MonsterFaceSprites[value / 34];
    }

    public void SetAmmo(int value)
    {
        value = value / 4;
        if (value > 49) value = 49;
        Ammo.sprite = AmmoSprites[value];
    }

    public void SetRecipe(string value)
    {
        if (value == "SNIPER")
        {
            Recipe.sprite = RecipeSniper;
        }
        else if (value == "ROCKET")
        {
            Recipe.sprite = RecipeRocket;
        }
    }

}
