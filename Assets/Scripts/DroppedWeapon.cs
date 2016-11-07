using UnityEngine;
using System.Collections;





public class DroppedWeapon : MonoBehaviour
{



    public string spritedroppedWeapon;
    public bool numberOfAmmoRand = false;
    public int numberOfAmmo = 50;
    public int numberOfAmmoMin = 10;
    public int numberOfAmmoMax = 70;

    public WEAPON_TYPE weaponType;

    void Start ()
    {
        if (numberOfAmmoRand)
        {
            numberOfAmmo = Random.Range(numberOfAmmoMin, numberOfAmmoMax);
        }
    }
}
