using UnityEngine;
using System.Collections;



public enum WEAPON_TYPE
{
    BASIC,
    FLAME_THROWER
}


public class Weapon : MonoBehaviour
{

    
    private WEAPON_TYPE type;
    [HideInInspector]
    public int bulletsLeft;

    private SpriteRenderer myRenderer;


    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetType(WEAPON_TYPE t)
    {
        switch(t)
        {
            case WEAPON_TYPE.BASIC:
                //myRenderer.sprite = Resources.Load<Sprite>("IMG_" + WEAPON_TYPE.BASIC.ToString());
                break;
        }
    }

    public WEAPON_TYPE GetWeaponType()
    {
        return type;
    }
    
}
